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

    [Header("Current Round")]
    [SerializeField] private List<ColorCube.CubeColor> currentSequence = new List<ColorCube.CubeColor>();

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
        StartRound();
    }

    public void RegisterColor(ColorCube.CubeColor color)
    {
        if (!canInput)
            return;

        if (color == currentSequence[currentIndex])
        {
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

        currentRound++;

        currentIndex = 0;

        if (currentRound > 4)
        {
            SimonFinished();
            return;
        }

        Invoke(nameof(StartRound), 2f);

        // Aquí després:
        // Mostrar número secret
        // Següent ronda
    }

    void FailedRound()
    {
        Debug.Log("ERROR");

        currentIndex = 0;

        StartCoroutine(ShowSequence());
    }

    void StartRound()
    {
        currentSequence.Clear();

        int sequenceLength = 3;

        if (currentRound == 2)
            sequenceLength = 4;

        if (currentRound >= 3)
            sequenceLength = 5;

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
    }
}
