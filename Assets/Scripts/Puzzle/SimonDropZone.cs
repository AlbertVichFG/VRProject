using UnityEngine;

public class SimonDropZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ColorCube cube =
            other.GetComponent<ColorCube>();

        if (cube == null)
            return;

        SimonGame.Instance.RegisterColor(cube.cubeColor);

        cube.ReturnToStart();
    }
}
