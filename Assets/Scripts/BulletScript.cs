using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject target;
    private int damage;
    public float speed = 15f;


    public void Seek(GameObject _target, int _damage)
    {
        target = _target;
        damage = _damage;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        
        Debug.Log("Bullet hit target for " + damage + " damage!");

        EnemyPathFollow enemyScript = target.GetComponent<EnemyPathFollow>();
        
        if (enemyScript != null)
        {
            enemyScript.Current_Health -= damage;
        }
        else
        {
            Debug.LogWarning("The target has no EnemyHealth script attached to it!");
        }
            
        Destroy(gameObject); 
    }
}