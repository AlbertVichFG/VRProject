using UnityEngine;


public class PoisonGas : MonoBehaviour
{
    [SerializeField] private float duration = 3f;
    [SerializeField] private float damagePerSecond = 5f;

    private void Start()
    {
        Destroy(gameObject, duration);
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerHealth player = other.GetComponentInParent<PlayerHealth>();

        if (player == null)
            return;

        player.TakeDamage(damagePerSecond * Time.deltaTime);
    }
}