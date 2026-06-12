using UnityEngine;

public class EnemyHandHitBox : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float knockbackForce = 0.3f;

    [SerializeField] private EnemyMelee enemy;

    private float attackCooldown = 1.5f;
    private float lastAttackTime;
    private PlayerHealth player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (Time.time < lastAttackTime + attackCooldown)
            return;

        if (player == null)
            return;

        player.TakeDamage(damage);

        Vector3 direction =
            (player.transform.position -
             enemy.transform.position).normalized;

        player.Knockback(direction, knockbackForce);

        lastAttackTime = Time.time;
    }
}
