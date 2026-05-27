using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTowerClass : MonoBehaviour
{

    public int Max_Health = 100;
    public int Current_Health = 0;

    public float Shooting_Range = 67.53f;

    public float Fire_Rate = 3.0f;

    public int Attack_DMG = 5;

    public GameObject AttackRange;

    public bool HitAttackRange = false;

    public bool InGame = false;

    public GameObject Self;

    private Manager gameManager;

    private GameObject Bullet;

    void Start()
    {
        gameManager = Object.FindFirstObjectByType<Manager>(); 
        Bullet = gameManager.Bullet;
    }

    void StartGame()
    {
        Current_Health = Max_Health;
        InGame = true;
        Self.SetActive(true);
    }

    void EndGame()
    {
        InGame = false;
        Self.SetActive(false);
    }

    void Update()
    {

        if (InGame == false) return;

        AttackRange.transform.localScale = new Vector3(Shooting_Range, Shooting_Range, Shooting_Range);

        if (transform.parent.CompareTag("Placed_Towers_Parent"))
        {

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            HitAttackRange = false;

            HitAttackRange = hit != null && hit.name == "AttackRange";

            transform.Find("AttackRange").GetComponent<SpriteRenderer>().enabled = HitAttackRange;

            Transform circle = transform.Find("Circle");


            if (circle != null)
            {
                if (HitAttackRange == false)
                {
                    circle.gameObject.SetActive(false);
                }
                else
                {
                    circle.gameObject.SetActive(true);
                }
            }


            
            
        }

    }
}
