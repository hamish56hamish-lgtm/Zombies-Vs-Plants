using UnityEngine;

public class PathManager : MonoBehaviour
{
    public Transform[] waypoints;

    void Awake()
    {
        waypoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}