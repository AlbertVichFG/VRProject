using UnityEngine;

public class ActiveSimons : MonoBehaviour
{
    private bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
        {
            Debug.Log("Simon already activated");
            return;

        }

        if (!other.CompareTag("Player"))
        {
            Debug.Log("Not player");
            return;
        }

        activated = true;

        SimonGame.Instance.StartSimon();
    }
}
