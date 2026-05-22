using UnityEngine;

public class Melee : EnemyController
{/*
    [Header("Melee Attack")]
    public float damage = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    [Header("Knockback")]
    public float knockbackForce = 0.3f;

    private float attackTimer;

    private PlayerHealth playerHealth;

    protected override void Start()
    {
        base.Start();

        playerHealth = player.GetComponent<PlayerHealth>();
    }

    protected override void Update()
    {
        base.Update();

        attackTimer += Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            Attack();
        }
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
        CharacterController cc = player.GetComponent<CharacterController>();

        if (cc != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            cc.Move(direction * knockbackForce);
        }
    }*/
}
