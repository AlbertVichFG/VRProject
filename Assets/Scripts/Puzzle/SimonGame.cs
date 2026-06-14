using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimonGame : MonoBehaviour
{
    public static SimonGame Instance;

    [Header("Simon Screen")]
    [SerializeField] private Image screenImage;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private TMP_Text codeText;
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private int numColors;

    [Header("Current Round")]
    [SerializeField] private List<ColorCube.CubeColor> currentSequence = new List<ColorCube.CubeColor>();


    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip correctSFX;
    [SerializeField] private AudioClip errorSFX;

    private int currentIndex;

    private bool canInput;

    private int currentRound = 1;


    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField]
    private int[] secretCode = { 1, 8, 9, 9 };

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateProgressText();

        screenImage.color = Color.black;

        codeText.text = "";

        canInput = false;

    }


    public void StartSimon()
    {
        StartRound();
    }

    void UpdateProgressText()
    {
        string result = "";

        for (int i = 0; i < 4; i++)
        {
            if (i < currentRound - 1)
                result += "X";
            else
                result += "?";
        }

        progressText.text = result;
    }

    public void RegisterColor(ColorCube.CubeColor color)
    {
        if (!canInput)
            return;

        if (color == currentSequence[currentIndex])
        {
            audioSource.PlayOneShot(correctSFX);

            currentIndex++;

            Debug.Log("Correcte");

            if (currentIndex >= currentSequence.Count)
            {
                RoundCompleted();
            }
        }
        else
        {
            FailedRound();
        }
    }

    void RoundCompleted()
    {
        Debug.Log("RONDA COMPLETADA");

        canInput = false;

        Debug.Log("Digit: " + secretCode[currentRound - 1]);

        StartCoroutine(ShowCodeDigit(secretCode[currentRound - 1]));

        currentRound++;

        UpdateProgressText();

        currentIndex = 0;

        if (currentRound > 4)
        {
            SimonFinished();
            return;
        }

        Invoke(nameof(StartRound), 2f);
    }

    void FailedRound()
    {
        Debug.Log("ERROR");

        audioSource.PlayOneShot(errorSFX);

        currentIndex = 0;

        StartCoroutine(ShowSequence());
    }

    void StartRound()
    {
        currentSequence.Clear();

        int sequenceLength = numColors;

        for (int i = 0; i < sequenceLength; i++)
        {
            currentSequence.Add((ColorCube.CubeColor)Random.Range(0, 4));
        }

        StartCoroutine(ShowSequence());
    }

    IEnumerator ShowSequence()
    {
        canInput = false;

        screenImage.color = Color.black;

        yield return new WaitForSeconds(1f);

        foreach (var color in currentSequence)
        {
            screenImage.color = GetUnityColor(color);

            yield return new WaitForSeconds(1f);

            screenImage.color = Color.black;

            yield return new WaitForSeconds(0.5f);
        }

        canInput = true;
    }

    Color GetUnityColor(ColorCube.CubeColor color)
    {
        switch (color)
        {
            case ColorCube.CubeColor.Red:
                return Color.red;

            case ColorCube.CubeColor.Green:
                return Color.green;

            case ColorCube.CubeColor.Blue:
                return Color.blue;

            case ColorCube.CubeColor.Yellow:
                return Color.yellow;
        }

        return Color.white;
    }

    void SimonFinished()
    {
        Debug.Log("SIMON Fi!");
        // Aquí activar zombieeessss.

        enemySpawner.StartZombieMode();

        MusicManager.Instance.PlayZombieMusic();
    }

    IEnumerator ShowCodeDigit(int digit)
    {
        codeText.text = digit.ToString();

        yield return new WaitForSeconds(2f);

        codeText.text = "";
    }


    public void ShowCodeMessage()
    {
        StartCoroutine(CodeMessageRoutine());
    }

    IEnumerator CodeMessageRoutine()
    {
        objectiveText.text = "INTODUEIX EL CODI PER GUANYAR";

        yield return new WaitForSeconds(5f);

        objectiveText.text = "";
    }
}
