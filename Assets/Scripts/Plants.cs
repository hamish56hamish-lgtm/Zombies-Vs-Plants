using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFollow : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float reachDistance = 0.1f;

    public int Max_Health = 5;
    public int Current_Health = 0;

    private Manager gameManager;
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    void Start()
    {

        Current_Health = Max_Health;



        gameManager = Object.FindFirstObjectByType<Manager>(); 

        PathManager path = FindFirstObjectByType<PathManager>();

        if (path != null)
        {
            waypoints = path.waypoints;
            transform.position = waypoints[0].position;
        }
        else
        {
            Debug.LogError("No PathManager found in scene!");
        }
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform target = waypoints[currentWaypointIndex];

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, target.position) < reachDistance)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                Destroy(gameObject);

                gameManager.KilledPerWave += 1;
                gameManager.TakeDamage(Current_Health);
                gameManager.UpdateUI();

            }
        }

        if (Current_Health < 0)
        {
            Death();
        }


    }
    public void Death()
    {
        Destroy(gameObject);
        gameManager.KilledPerWave += 1;
        gameManager.Cash += 150;
        gameManager.UpdateUI();
        gameManager.KilledAEnemy();

    }


}
