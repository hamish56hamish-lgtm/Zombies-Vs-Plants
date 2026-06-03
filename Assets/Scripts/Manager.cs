using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject GameUI;

    public GameObject HoldingManager;

    public GameObject GameLevelStuff;

    public int Max_Health = 100;
    public int Current_Health = 0;

    public int Cash = 1000;

    public TMP_Text HealthText;
    public TMP_Text CashText;

    public TMP_Text WaveText;

    public GameObject Bullet;

    public int Wave = 1;

    public GameObject EnemysManager;

    [System.Serializable]
    public class EnemySpawnData
    {
        public string enemyName;
        public GameObject prefab;
        public float spawnDelay = 1f;
    }
    public EnemySpawnData[] enemyTypes;

    public bool InGame;

    private bool FinishedSpawning = true;

    private int SpawnAmount = 10;

    public int KilledPerWave = 0;

    private int EnemyToSpawn = 0;


    void Start()
    {
        InGame = false;
        MainMenuStart();
    }

    void Update()
    {
        if (KilledPerWave == SpawnAmount && FinishedSpawning)
        {
            KilledPerWave = 0;
            StartSpawning();
            Wave += 1;
            UpdateUI();
        }
    }

    void MainMenuStart()
    {
        MainMenu.SetActive(true);
        GameUI.SetActive(false);
        GameLevelStuff.SetActive(false);
    }

    public void StartGame()
    {
        MainMenu.SetActive(false);
        GameUI.SetActive(true);
        GameLevelStuff.SetActive(true);
        Current_Health = Max_Health;

        UpdateUI();
        StartSpawning();
        InGame = true;
        UpdateUI();
    }

    public void TakeDamage(int DamageAmount)
    {
        Current_Health -= DamageAmount;
        UpdateUI();
    }

    public void Death()
    {
        Debug.Log("You have died! lol");
    }

    public void UpdateUI()
    {
        HealthText.text = Current_Health.ToString();
        CashText.text = "$" + Cash.ToString();
        WaveText.text = "Wave " + Wave.ToString();
    }

    public void StartSpawning()
    {
        if (FinishedSpawning == true)
        {
            StartCoroutine(SpawnEnemies());
        }
        else
        {
            Debug.Log("Must wait to spawn more");
        }
    }

    IEnumerator SpawnEnemies()
    {
        FinishedSpawning = false;
        yield return new WaitForSeconds(5f);

        SpawnAmount = 5 * Wave;

        for (int i = 0; i < SpawnAmount; i++)
        {
            if (enemyTypes == null || enemyTypes.Length == 0) yield break;

            if (Wave < 2)
            {
                EnemyToSpawn = 0;
            }
            else if (Wave >= 2 && Wave < 5)
            {
                EnemyToSpawn = Random.Range(0, 2);
            }
            else
            {
                EnemyToSpawn = Random.Range(0, enemyTypes.Length);
            }

            EnemySpawnData chosenEnemy = enemyTypes[EnemyToSpawn];

            Instantiate(
                chosenEnemy.prefab,
                transform.position,
                Quaternion.identity,
                EnemysManager.transform
            );

            yield return new WaitForSeconds(chosenEnemy.spawnDelay);
        }
        FinishedSpawning = true;
    }

    public void KilledAEnemy()
    {
        Debug.Log("Killed Already: " + KilledPerWave.ToString() + "Spawned Amount: " + SpawnAmount.ToString());
    }
}