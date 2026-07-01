using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTowerClass : MonoBehaviour
{


    public List<GameObject> enemiesInRange = new List<GameObject>();

    private SpriteRenderer spriteRenderer;

    public int Max_Health = 100;
    public int Current_Health = 0;

    public float Shooting_Range = 67.53f;

    public float Fire_Rate = 3.0f;
    private float fireCountdown = 0f;

    public int Attack_DMG = 5;

    public GameObject AttackRange;

    public bool HitAttackRange = false;

    public bool InGame = false;

    private Manager gameManager;

    private GameObject Bullet;




    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }
    void Start()
    {
        gameManager = Object.FindFirstObjectByType<Manager>();
        Bullet = gameManager.Bullet;
        InGame = gameManager.InGame;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        StartGame();
    }

    void StartGame()
    {
        Current_Health = Max_Health;
        InGame = true;
        gameObject.SetActive(true);
    }

    void EndGame()
    {
        InGame = false;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameManager != null)
        {
            InGame = gameManager.InGame;
        }


        if (gameManager.Dead)
        {
            EndGame();
        }

        if (InGame == false) return;
        AttackRange.transform.localScale = new Vector3(Shooting_Range, Shooting_Range, Shooting_Range);

        CountEnemysInRange();


        if (transform.parent.CompareTag("Placed_Towers_Parent"))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            HitAttackRange = false;

            if (hit != null && hit.name == "AttackRange")
            {
                if (hit.transform.parent == this.transform)
                {
                    HitAttackRange = true;
                }
            }

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
    private void CountEnemysInRange()
    {
        enemiesInRange.RemoveAll(enemy => enemy == null);

        if (fireCountdown > 0f)
        {
            fireCountdown -= Time.deltaTime;
        }

        if (enemiesInRange.Count > 0)
        {
            GameObject target = enemiesInRange[0];
            
            if (target != null)
            {
                if (fireCountdown <= 0f)
                {
                    Shoot(target);
                    fireCountdown = 1f / Fire_Rate;
                }
            }
        }
    }

    private void Shoot(GameObject target)
    {
        if (Bullet == null) return;

        // Flip tower based on target direction
        if (spriteRenderer != null)
        {
            if (target.transform.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true;  // face left
            }
            else
            {
                spriteRenderer.flipX = false; // face right
            }
        }

        GameObject bulletGO = Instantiate(Bullet, transform.position, Quaternion.identity);

        BulletScript bulletScript = bulletGO.GetComponent<BulletScript>();

        if (bulletScript != null)
        {
            bulletScript.Seek(target, Attack_DMG);
        }
    }

}
