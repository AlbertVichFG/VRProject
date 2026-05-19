using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float damage = 25f;

    private void OnEnable()
    {
        Invoke(nameof(DisableBullet), 5f);
    }

    void DisableBullet()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyController enemy =
            collision.gameObject.GetComponentInParent<EnemyController>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        gameObject.SetActive(false);
    }
}
