using UnityEngine;

public class Granade : MonoBehaviour
{
    [SerializeField] private GameObject pinObject;

    [SerializeField] private ParticleSystem explosionParticles;

    [SerializeField] private float fuseTime = 3f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float damage = 50f;


    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip explosionSFX;

    [SerializeField] private bool pinRemoved;

    public void RemovePin()
    {
        Debug.Log("PIN REMOVED");

        if (pinRemoved)
            return;

        pinRemoved = true;

        pinObject.transform.SetParent(null);

        Rigidbody rb = pinObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        Invoke(nameof(Explode), fuseTime);
    }

    void Explode()
    {

        Debug.Log("Granade exploded!");

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in hits)
        {
            IDamageable damageable = hit.GetComponentInParent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }

        AudioSource.PlayClipAtPoint(explosionSFX, transform.position);

        explosionParticles.transform.SetParent(null);

        explosionParticles.Play();

        Destroy(explosionParticles.gameObject, 3f);

        Destroy(gameObject);
    }
}
