using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Explosione : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 50f;
    private float currentHealth;

    [Header("Movement")]
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 4f;

    [Header("Explosion")]
    [SerializeField] private float explosionRange = 2.5f;
    [SerializeField] private float explosionDamage = 30f;
    [SerializeField] private float knockbackForce = 0.5f;
    [SerializeField] private float explodeDelay = 1f;
    [SerializeField] private GameObject poisonGasPrefab;

    [SerializeField] private bool exploding;

    private Animator animator;
    private NavMeshAgent agent;
    private bool exploded;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (player == null)
        {
            GameObject xrOrigin =
                GameObject.FindGameObjectWithTag("Player");

            if (xrOrigin != null)
            {
                player = xrOrigin.transform;
            }
        }

        currentHealth = maxHealth;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    private void Update()
    {
        if (exploded || player == null)
            return;

        agent.SetDestination(player.position);

        if (animator != null)
            animator.SetBool("Running", true);

        float distance =
            Vector3.Distance(transform.position, player.position);

        if (distance <= explosionRange && !exploding)
        {
            if (animator != null)
                animator.SetBool("Running", false);

            StartCoroutine(ExplodeRoutine());
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        if (exploded)
            return;

        exploded = true;

        float distance =
            Vector3.Distance(transform.position, player.position);

        if (distance <= explosionRange)
        {
            PlayerHealth playerHealth = FindFirstObjectByType<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(explosionDamage);

                Vector3 direction =
                    (player.position - transform.position).normalized;

                playerHealth.Knockback(
                    direction,
                    knockbackForce
                );
            }
        }

        Instantiate(poisonGasPrefab,transform.position, Quaternion.identity);

        EnemySpawner.Instance.EnemyKilled();

        Destroy(gameObject, 1.5f);
    }

    IEnumerator ExplodeRoutine()
    {
        exploding = true;

        agent.isStopped = true;

        if (animator != null)
            animator.SetTrigger("Death");

        yield return new WaitForSeconds(explodeDelay);

        Explode();
    }
}