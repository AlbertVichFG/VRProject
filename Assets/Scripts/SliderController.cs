using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class SliderController : MonoBehaviour
{
    [SerializeField]
    private InputActionReference grabLeft, grabRight;
    private Vector3 initialPos;

    [SerializeField]
    private float limitSlider;
    [SerializeField]
    private Transform leftHand, rightHand;
    private Vector3 handPos;

    [SerializeField]
    private WeaponController weaponController;

    private Transform usedHand;
    private bool isGrabbed;



    void Start()
    {
        initialPos = transform.localPosition;


    }

    void SliderGrabed(InputAction.CallbackContext context)
    {
        handPos = usedHand.position;
        isGrabbed = true;

        StopAllCoroutines();
    }

    void SliderRelease(InputAction.CallbackContext context)
    {
        isGrabbed = false;
        //   transform.localPosition = initialPos;
        StartCoroutine(ReturnToInitialPos());
        weaponController.PlayBoltSound();
        //  grabLeft.action.canceled -= SliderRelease;
        //  grabRight.action.canceled -= SliderRelease;
    }

    IEnumerator ReturnToInitialPos()
    {
        Vector3 startPos = transform.localPosition;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 5; // Adjust the speed of the return as needed
            transform.localPosition = Vector3.Lerp(startPos, initialPos, t);
            // Debug.Log("Returning to initial position: ");

            yield return null;
        }
    }

    void Update()
    {
        if (isGrabbed)
        {
            Vector3 newHandPos = usedHand.position;
            Vector3 deltaPos = newHandPos - handPos;
            float distance = Mathf.Clamp(deltaPos.magnitude, 0, limitSlider);
            transform.localPosition = initialPos + new Vector3(0, 0, distance);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftHand"))
        {
            grabLeft.action.performed += SliderGrabed;
            grabLeft.action.canceled += SliderRelease;
            usedHand = leftHand;
        }
        else if (other.CompareTag("RightHand"))
        {
            grabRight.action.performed += SliderGrabed;
            grabRight.action.canceled += SliderRelease;
            usedHand = rightHand;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftHand"))
        {
            grabLeft.action.performed -= SliderGrabed;
        }
        else if (other.CompareTag("RightHand"))
        {
            grabRight.action.performed -= SliderGrabed;
        }
    }


    public void Recoil()
    {
        StopAllCoroutines();

        StartCoroutine(RecoilRoutine());
    }

    IEnumerator RecoilRoutine()
    {
        Vector3 originalPos = transform.localPosition;

        transform.localPosition =
            originalPos + new Vector3(0, 0, -0.02f);

        yield return new WaitForSeconds(0.03f);

        transform.localPosition = originalPos;
    }

}
