using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Disparador : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 80f;
    private float currentHealth;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform firePoint;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootDelay = 0.5f;
    [SerializeField] private float bulletSpeed = 20f;

    [Header("Combat")]
    [SerializeField] private float shootRange = 8f;
    [SerializeField] private float retreatRange = 4f;
    [SerializeField] private float fireRate = 1.5f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;

    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField]
    private bool isAttacking;


    private float shootTimer;

    private void Start()
    {
        if (player == null)
        {
            GameObject xrOrigin = GameObject.FindGameObjectWithTag("Player");

            if (xrOrigin != null)
            {
                player = xrOrigin.transform;
            }
        }


        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    private void Update()
    {
        if (player == null)
            return;

        shootTimer += Time.deltaTime;

        float distance =
            Vector3.Distance(transform.position, player.position);

        Vector3 lookPos = player.position - transform.position;
        lookPos.y = 0;

        if (lookPos != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookPos);

            transform.rotation = Quaternion.Slerp(transform.rotation,
                targetRotation, 5f * Time.deltaTime);
        }

        if (distance > shootRange)
        {
            agent.SetDestination(player.position);

            if (animator != null)
                animator.SetBool("Running", true);

            Debug.Log("Moving towards player");
        }
        else if (distance < retreatRange)
        {
            Vector3 direction =
                (transform.position - player.position).normalized;

            Vector3 retreatPosition =
                transform.position + direction * 3f;

            agent.SetDestination(retreatPosition);
        }
        else
        {
            agent.ResetPath();

            if (animator != null)
                animator.SetBool("Running", false);

            if (shootTimer >= fireRate)
            {
                StartCoroutine(ShootRoutine());
                shootTimer = 0;
            }
        }
    }

    void SpawnBullet()
    {
        if (animator != null)
        {
            animator.SetInteger("AttackIndex", Random.Range(1, 4));

            animator.SetTrigger("Attack");

            Debug.Log("Disparando");
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.linearVelocity = firePoint.forward * bulletSpeed;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            EnemySpawner.Instance.EnemyKilled();

            Destroy(gameObject);
        }
    }

    IEnumerator ShootRoutine()
    {
        if (animator != null)
        {
            animator.SetInteger(
                "AttackIndex",
                Random.Range(1, 4)
            );

            animator.SetTrigger("Attack");
        }

        yield return new WaitForSeconds(shootDelay);

        SpawnBullet();
    }
}