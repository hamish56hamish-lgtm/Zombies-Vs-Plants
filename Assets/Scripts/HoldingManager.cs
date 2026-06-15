using UnityEditor.UI;
using UnityEngine;
using System.Collections.Generic;

public class MouseFollowManager : MonoBehaviour
{
    private Transform child;

    public Camera MainCamera;

    [SerializeField] private GameObject holdingTower;

    private Manager gameManager;
    public GameObject WizardTowerPrefab;
    public GameObject PyroZombiePrefab;

    public Transform PlacedTowers;

    public float placementRadius = 0.5f;
    public LayerMask towerLayer;

    public bool InGame = false;

    public GameObject HoldingManager;

    public int Tower_Cost = 0;

    private Dictionary<string, int> prefabCosts = new Dictionary<string, int>
    {
        { "WizardTowerPrefab", 500 },
        { "PyroZombiePrefab", 250 }
    };
    void Start()
    {
        HoldingManager.SetActive(false);
        gameManager = Object.FindFirstObjectByType<Manager>(); 
    }
    public void StartGame()
    {
        holdingTower = WizardTowerPrefab;
        Tower_Cost = prefabCosts["WizardTowerPrefab"];
        SetChildPrefab(holdingTower);
        InGame = true;
        HoldingManager.SetActive(true);
        
    }

    void EndGame()
    {
        InGame = false;
        HoldingManager.SetActive(false);

    }


    void Update()
    {



        if (InGame == false) return;


        if (child == null) return;

        Vector3 mousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        child.position = mousePos;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            holdingTower = WizardTowerPrefab;
            Tower_Cost = prefabCosts["WizardTowerPrefab"];
            SetChildPrefab(holdingTower);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            holdingTower = PyroZombiePrefab;
            Tower_Cost = prefabCosts["PyroZombiePrefab"];
            SetChildPrefab(holdingTower);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;

            Collider2D[] hits = Physics2D.OverlapCircleAll(
                pos,
                placementRadius
            );

            bool canPlace = true;

            foreach (Collider2D hit in hits)
            {
                if (((1 << hit.gameObject.layer) & towerLayer) != 0)
                {
                    canPlace = false;
                    break;
                }

                if (hit.CompareTag("Block"))
                {
                    canPlace = false;
                    break;
                }
            }

            if (gameManager.Cash >= Tower_Cost)
            {
                canPlace = true;
            }
            else
            {
                canPlace = false;
                return;
            }

            if (canPlace)
            {
                gameManager.Cash -= Tower_Cost;
                gameManager.UpdateUI();
                Instantiate(
                    holdingTower,
                    pos,
                    Quaternion.identity,
                    PlacedTowers
                );



                
            }
            else
            {
                Debug.Log("Can't place here!");
            }
        }
    }

    public void SetChildPrefab(GameObject newPrefab)
    {
        foreach (Transform children in transform)
        {
            Destroy(children.gameObject);
        }

        GameObject obj = Instantiate(newPrefab, transform);

        child = obj.transform;

        child.localPosition = Vector3.zero;
        child.localRotation = Quaternion.identity;

        Collider2D[] colliders = obj.GetComponentsInChildren<Collider2D>();

        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (child == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(child.position, placementRadius);
    }
}