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

    // Start is called before the first frame update
    void Start()
    {
        Current_Health = Max_Health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
