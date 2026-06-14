using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMelee : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3.5f;

    [Header("Melee Attack")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackRange = 1.8f;
    [SerializeField] private float attackCooldown = 1.5f;

    [Header("Knockback")]
    [SerializeField] private float knockbackForce = 0.3f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip idleZombieSFX;

    private float attackTimer;
    private bool isAttacking;


    private Animator animator;
    private PlayerHealth playerHealth;
    private NavMeshAgent agent;

    private bool isDead;

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

        audioSource.clip = idleZombieSFX;
        audioSource.loop = true;
        audioSource.Play();

        currentHealth = maxHealth;

        playerHealth = player.GetComponentInChildren<PlayerHealth>();

        animator = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.stoppingDistance = attackRange - 0.2f;

        Debug.Log(player);
    }

    private void Update()
    {
      

        if (isDead || player == null || isAttacking)
            return;

        attackTimer += Time.deltaTime;

        float distance =
            Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            agent.SetDestination(player.position);

            if (animator != null)
                animator.SetBool("Walking", true);
        }
        else
        {
            agent.ResetPath();

            if (animator != null)
                animator.SetBool("Walking", false);

            Attack();
        }
    }

    void Attack()
    {
        if (attackTimer >= attackCooldown)
        {
            StartCoroutine(AttackRoutine());

            attackTimer = 0;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        if (isDead)
            return;

        isDead = true;

        audioSource.Stop();
        Destroy(audioSource);

        agent.enabled = false;

        if (animator != null)
            animator.SetTrigger("Die");

        EnemySpawner.Instance.EnemyKilled();

        Destroy(gameObject, 3f);
    }


    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        agent.isStopped = true;

        if (animator != null)
            animator.SetTrigger("Attack");

        yield return new WaitForSeconds(1f);

        agent.isStopped = false;

        isAttacking = false;
    }
}