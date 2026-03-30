using UnityEngine;

public class MouseFollowManager : MonoBehaviour
{
    private Transform child;

    public Camera MainCamera;

    [SerializeField] private GameObject holdingTower;
    public GameObject WizardTowerPrefab;

    public GameObject PyroZombiePrefab;
    
    public Transform PlacedTowers;

    void Start()
    {
        holdingTower = WizardTowerPrefab;


        SetChildPrefab(holdingTower);
    }

    void Update()
    {
        if (child == null) return;

        Vector3 mousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        child.position = mousePos;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            holdingTower = WizardTowerPrefab;
            SetChildPrefab(holdingTower);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            holdingTower = PyroZombiePrefab;
            SetChildPrefab(holdingTower);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;

            Instantiate(holdingTower, pos, Quaternion.identity, PlacedTowers);
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
    }
}