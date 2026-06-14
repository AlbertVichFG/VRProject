using UnityEngine;


public class PoisonGas : MonoBehaviour
{
    [SerializeField] private float damagePerSecond = 10f;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("TOCANT: " + other.name);

        if (!other.CompareTag("Player"))
            return;

        PlayerHealth player = FindFirstObjectByType<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}