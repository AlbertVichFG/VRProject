using UnityEngine;

public class SimonDropZone : MonoBehaviour
{

    [SerializeField] private bool isProcessing;

    private void OnTriggerEnter(Collider other)
    {
        if (isProcessing)
            return;

        ColorCube cube = other.GetComponent<ColorCube>();

        if (cube == null)
            return;

        isProcessing = true;

        SimonGame.Instance.RegisterColor(cube.cubeColor);

        cube.ReturnToStart();

      //  cubeCollider.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        ColorCube cube = other.GetComponent<ColorCube>();

        if (cube != null)
        {
            isProcessing = false;
        }
    }
}
