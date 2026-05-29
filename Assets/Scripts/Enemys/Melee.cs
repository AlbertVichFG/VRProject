using UnityEngine;

public class EnemyMelee : EnemyController
{
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Melee Attack")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1.5f;

    [Header("Knockback")]
    [SerializeField] private float knockbackForce = 0.3f;

    private float attackTimer;

    private PlayerHealth playerHealth;

    protected override void Start()
    {
        base.Start();

        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            Attack();
        }

        // animator.SetBool("Walking", true);
    }

    void Attack()
    {
        if (attackTimer >= attackCooldown)
        {
            playerHealth.TakeDamage(damage);

            KnockbackPlayer();

            attackTimer = 0;
        }
    }

    void KnockbackPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        playerHealth.Knockback(direction, knockbackForce);
    }
}