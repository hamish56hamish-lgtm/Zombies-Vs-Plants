using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFollow : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float reachDistance = 0.1f;

    private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    void Start()
    {
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
            }
        }
    }
}
