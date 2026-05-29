using UnityEngine;
using UnityEngine.AI;

public class Explosione : EnemyController
{
    [Header("Movement")]
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 4f;

    [Header("Explosion")]
    [SerializeField] private float explosionRange = 2.5f;
    [SerializeField] private float explosionDamage = 30f;
    [SerializeField] private float knockbackForce = 0.5f;

    private NavMeshAgent agent;
    private bool exploded;

    protected override void Start()
    {
        base.Start();

        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    private void Update()
    {
        if (exploded || player == null)
            return;

        agent.SetDestination(player.position);

        float distance =
            Vector3.Distance(transform.position, player.position);

        if (distance <= explosionRange)
        {
            Explode();
        }

        // animator.SetBool("Walking", true);
    }

    protected override void Die()
    {
        Explode();
    }

    private void Explode()
    {
        if (exploded)
            return;

        exploded = true;

        // animator.SetTrigger("Explode");

        float distance =
            Vector3.Distance(transform.position, player.position);

        if (distance <= explosionRange)
        {
            PlayerHealth playerHealth =
                player.GetComponent<PlayerHealth>();

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

        Destroy(gameObject);
    }
}

