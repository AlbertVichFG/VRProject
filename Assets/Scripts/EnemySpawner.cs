using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [Header("Enemies")]
    [SerializeField] private GameObject meleePrefab;
    [SerializeField] private GameObject exploderPrefab;
    [SerializeField] private GameObject shooterPrefab;
    [SerializeField] private float spawnDelay;

    [SerializeField] private GameObject keypadObject;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Debug")]
    [SerializeField] private bool skipSimon;

    private int enemiesAlive;
    private int currentRound;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (skipSimon)
        {
            skipSimon = false;

            StartZombieMode();
        }
    }

    public void StartZombieMode()
    {
        Debug.Log("Zombie Mode Started");

        currentRound = 1;

        StartRound();
    }

    void StartRound()
    {
        Debug.Log("ROUND " + currentRound);

        switch (currentRound)
        {
            case 1:
                StartCoroutine(Round1());
                break;

            case 2:
                StartCoroutine(Round2());
                break;

            case 3:
                StartCoroutine(Round3());
                break;
        }
    }

    IEnumerator Round1()
    {
        enemiesAlive = 8;

        for (int i = 0; i < 8; i++)
        {
            SpawnEnemy(meleePrefab);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator Round2()
    {
        enemiesAlive = 9;

        for (int i = 0; i < 6; i++)
        {
            SpawnEnemy(meleePrefab);

            yield return new WaitForSeconds(spawnDelay);
        }

        for (int i = 0; i < 3; i++)
        {
            SpawnEnemy(exploderPrefab);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator Round3()
    {
        enemiesAlive = 13;

        for (int i = 0; i < 8; i++)
        {
            SpawnEnemy(meleePrefab);

            yield return new WaitForSeconds(spawnDelay);
        }

        for (int i = 0; i < 4; i++)
        {
            SpawnEnemy(exploderPrefab);

            yield return new WaitForSeconds(spawnDelay);
        }

        SpawnEnemy(shooterPrefab);
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        int randomSpawn = Random.Range(0, spawnPoints.Length);

        Instantiate(enemyPrefab, spawnPoints[randomSpawn].position, Quaternion.identity);
    }

    public void EnemyKilled()
    {
        enemiesAlive--;

        Debug.Log("Enemies Alive: " + enemiesAlive);

        if (enemiesAlive <= 0)
        {
            RoundCompleted();
        }

        if (currentRound == 3 && enemiesAlive <= 0)
        {
            keypadObject.SetActive(true);

            SimonGame.Instance.ShowCodeMessage();
        }
    }

    void RoundCompleted()
    {
        Debug.Log("ROUND COMPLETED");

        currentRound++;

        if (currentRound > 3)
        {
            AllRoundsCompleted();
            return;
        }

        Invoke(nameof(StartRound), 3f);
    }

    void AllRoundsCompleted()
    {
        Debug.Log("ALL ROUNDS COMPLETED");
        /*
        KeypadMaanger keypadManager =    keypadObject.GetComponentInChildren<KeypadMaanger>();

        keypadManager.UnlockKeypad();

        keypadObject.SetActive(true);

        SimonGame.Instance.ShowCodeMessage();*/

    }
}