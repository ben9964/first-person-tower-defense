using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    // Start is called before the first frame update
    private float damage;
    private float projectileSpeed;
    [SerializeField] private float range = 15f;

    [SerializeField] private Transform target;


    

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        damage = 1f;
        projectileSpeed = 1f;
        
    }

    void UpdateTarget()
    {
        // Get all enemies currently in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        // Temp vars to store nearest enemy and shortest distance to enemy
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // Loop through all enemies in the scene
        foreach (GameObject enemy in enemies)
        {
            // Calculate distance to each enemy and check if any are closer than another
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            return;
        } 

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
