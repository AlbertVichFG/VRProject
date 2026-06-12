using UnityEngine;

public class ActiveSimons : MonoBehaviour
{
    private bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        if (!other.CompareTag("Player"))
            return;

        activated = true;

        SimonGame.Instance.StartSimon();
    }
}
