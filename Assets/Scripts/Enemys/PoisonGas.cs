using UnityEngine;


public class PoisonGas : MonoBehaviour
{
    [SerializeField] private float damageTick = 5f;
    [SerializeField] private float tickRate = 1f;

    private float nextTick;

    private void OnTriggerStay(Collider other)
    {
        if (Time.time < nextTick)
            return;

        PlayerHealth player =
            FindFirstObjectByType<PlayerHealth>();

        if (player == null)
            return;

        player.TakeDamage(damageTick);

        nextTick = Time.time + tickRate;
    }
}