using UnityEngine;
using System.Collections;

public class ColorCube : MonoBehaviour
{
    public enum CubeColor
    {
        Red,
        Green,
        Blue,
        Yellow
    }

    public CubeColor cubeColor;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private Collider cubeCollider;

    private Rigidbody rb;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        cubeCollider = GetComponent<Collider>();

        rb = GetComponent<Rigidbody>();
    }

    public void ReturnToStart()
    {
        StartCoroutine(ReturnCoroutine());
    }

    IEnumerator ReturnCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
