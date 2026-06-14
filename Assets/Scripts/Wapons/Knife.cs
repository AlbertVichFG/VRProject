using UnityEngine;

public class Knife : MonoBehaviour
{
    private float lastHitTime;
    [SerializeField] private float hitCooldown = 0.5f;
    [SerializeField] private float damage = 25f;

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < lastHitTime + hitCooldown)
            return;

        IDamageable damageable =
            other.GetComponentInParent<IDamageable>();

        if (damageable == null)
            return;

        damageable.TakeDamage(damage);

        HitMark.Instance.ShowHitmarker();

        lastHitTime = Time.time;
    }
}
