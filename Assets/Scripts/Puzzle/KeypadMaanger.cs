using System.Collections;
using TMPro;
using UnityEngine;

public class KeypadMaanger : MonoBehaviour
{
    [SerializeField] private TextMeshPro codeText;
    [SerializeField] private GameObject winPanel;

    private string currentCode = "";

    [SerializeField]
    private string correctCode = "7241";

    private void Start()
    {
        codeText.text = "_ _ _ _";
    }

    public void AddNumber(string number)
    {

        if (currentCode.Length >= 4)
            return;

        currentCode += number;

        codeText.text = currentCode;
    }

    public void EnterCode()
    {
        if (currentCode == correctCode)
        {
            Debug.Log("WIN");

            codeText.text = "WIN";

            winPanel.SetActive(true);

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
}
