using UnityEngine;

public class MouseFollowManager : MonoBehaviour
{
    private Transform child;

    public Camera MainCamera;

    [SerializeField] private GameObject holdingTower;

    public GameObject WizardTowerPrefab;
    public GameObject PyroZombiePrefab;

    public Transform PlacedTowers;

    public float placementRadius = 0.5f;
    public LayerMask towerLayer;

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

            if (canPlace)
            {
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