using UnityEngine;

public class Granade : MonoBehaviour
{
    [SerializeField] private GameObject pinObject;

    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private float fuseTime = 3f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float damage = 50f;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip explosionSFX;

    private bool pinRemoved;

    public void RemovePin()
    {
        if (pinRemoved)
            return;

        pinRemoved = true;

        pinObject.transform.SetParent(null);

        Invoke(nameof(Explode), fuseTime);
    }

    void Explode()
    {


        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        foreach (Collider hit in hits)
        {
            IDamageable damageable = hit.GetComponentInParent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }

        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        AudioSource.PlayClipAtPoint(explosionSFX, transform.position);


        Destroy(explosion, 3f);

        Destroy(gameObject);
    }
}
