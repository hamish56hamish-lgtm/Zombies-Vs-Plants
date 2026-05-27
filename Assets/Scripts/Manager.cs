using System.Collections;
using System.Collections.Generic;
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

    public GameObject Bullet;

    public int Wave = 1;

    public GameObject EnemysManager;

    public List<GameObject> enemyPrefabs = new List<GameObject>();




    void Start()
    {
        
        MainMenuStart();



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

    }


    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log("Its Spawning?");

            if (enemyPrefabs.Count == 0) yield break;

            Instantiate(
                enemyPrefabs[0],
                transform.position,
                Quaternion.identity,
                EnemysManager.transform
            );

            yield return new WaitForSeconds(1f); // ⬅ delay between spawns
        }
    }


}
