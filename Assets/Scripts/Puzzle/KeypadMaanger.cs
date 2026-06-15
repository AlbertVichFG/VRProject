using System.Collections;
using TMPro;
using UnityEngine;

public class KeypadMaanger : MonoBehaviour
{
    [SerializeField] private TextMeshPro codeText;
    [SerializeField] private GameObject winPanel;

    [SerializeField] private bool canEnterCode;

    [SerializeField]
    private PauseMenu pauseMenu;

    private string currentCode = "";

    [SerializeField]
    private string correctCode = "7241";

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonSFX;

    private void Start()
    {
        codeText.text = "_ _ _ _";
    }

    public void AddNumber(string number)
    {

        if (currentCode.Length >= 4)
            return;

        audioSource.PlayOneShot(buttonSFX);

        currentCode += number;

        codeText.text = currentCode;
    }

    public void EnterCode()
    {
        if (!canEnterCode)
        {
            Debug.Log("Acaba les rondes primer!");
            return;
        }

        if (currentCode == correctCode)
        {
            Debug.Log("WIN");

            codeText.text = "WIN";

            pauseMenu.ShowPanel(winPanel);

            Time.timeScale = 0f;
        }
        else
        {
            Debug.Log("ERROR");

            StartCoroutine(ShowError());
        }
    }

    public void ClearCode()
    {
        currentCode = "";
        codeText.text = "----";
    }

    IEnumerator ShowError()
    {
        codeText.text = "ERROR";

        yield return new WaitForSeconds(2f);

        ClearCode();
    }


    public void UnlockKeypad()
    {
        canEnterCode = true;

        Debug.Log("Keypad desbloquejat");
    }
}
