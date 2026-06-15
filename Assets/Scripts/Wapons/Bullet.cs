using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float damage = 25f;

    [SerializeField]
    private float lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
      //  Debug.Log("He colisionat amb: " + collision.gameObject.name);

        IDamageable damageable = collision.gameObject.GetComponentInParent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);

            HitMark.Instance.ShowHitmarker();
        }

        Destroy(gameObject);
    }
}
