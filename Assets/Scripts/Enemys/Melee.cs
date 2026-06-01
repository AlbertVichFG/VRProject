using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : EnemyController
{
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

    private float attackTimer;

    [SerializeField]
    private bool isDead = false;

    private Animator animator;
    private PlayerHealth playerHealth;
    private NavMeshAgent agent;

    protected override void Start()
    {
        base.Start();

      //  animator.SetBool("Walking", true);

        playerHealth = player.GetComponent<PlayerHealth>();
        
        animator = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.stoppingDistance = attackRange - 0.2f;
    }

    private void Update()
    {
        if (player == null)
            return;

        attackTimer += Time.deltaTime;

        float distance =
            Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();
            Attack();
        }

        animator.SetBool("Walking", true);
    }

    void Attack()
    {
        if (attackTimer >= attackCooldown)
        {
            animator.SetTrigger("Attack");

            playerHealth.TakeDamage(damage);

            KnockbackPlayer();

            attackTimer = 0;
        }
    }

    void KnockbackPlayer()
    {
        Vector3 direction =
            (player.position - transform.position).normalized;

        playerHealth.Knockback(direction,knockbackForce);
    }

    protected override void Die()
    {
        if (isDead)
            return;

        isDead = true;

        agent.enabled = false;

      //  animator.SetBool("Walking", false);
        animator.SetTrigger("Die");

        Destroy(gameObject, 3f);
    }
}