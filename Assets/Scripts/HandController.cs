using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{

    private Animator animator;

    [SerializeField]
    private InputActionReference gripInput;
    [SerializeField]
    private InputActionReference triggerInput;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        float gripValue = gripInput.action.ReadValue<float>();
        float triggerValue = triggerInput.action.ReadValue<float>();

        animator.SetFloat("Grip", gripValue);
        animator.SetFloat("Trigger", triggerValue);
    }
}
