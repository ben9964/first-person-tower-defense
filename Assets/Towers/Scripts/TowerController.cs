using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float range = 15f;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float rateOfFire = 1000f;
    private float rotationSpeed = 5f;

    public Transform rotator;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        
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

        // Set target to nearest enemy if enemy exists in range
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
            // Instantiate(projectile);
            Vector3 dir = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(dir);
            Vector3 r = Quaternion.Lerp(rotator.rotation, rotation, Time.deltaTime * rotationSpeed).eulerAngles;
            rotator.rotation = Quaternion.Euler(0f, r.y, 0f);
        }


    }

    private void OnDrawGizmos()
    {
        // Draws the range of the tower around it
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
