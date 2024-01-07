using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // rateOfFire is shots PER SECOND.
    [Header("Attributes")]
    [SerializeField] private float rateOfFire = 1f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float fireCooldown = 0f;
    [SerializeField] private float range = 15f;
    [SerializeField] private float cost = 100f;

    [Header("Fields")]
    
    [SerializeField] private Transform target;
    private float rotationSpeed = 5f;
    public Transform rotator;
    [SerializeField] private GameObject projectile;
    public Transform projectileSpawn;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private ParticleSystem particleSystem;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0,0,0);
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
        if (target == null) return;
        
        Vector3 dir = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir);
        Vector3 r = Quaternion.Lerp(rotator.rotation, rotation, Time.deltaTime * rotationSpeed).eulerAngles;
        rotator.rotation = Quaternion.Euler(0f, r.y, 0f);

        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1000f / rateOfFire;
        }

        fireCooldown -= Time.deltaTime;
    }

    private void Shoot()
    {
        // Instantiate a projectile and call a script to target that projectile
        GameObject proj = (GameObject)Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
        Projectile p = proj.GetComponent<Projectile>();
        if (p != null)
        {
            p.SelectTarget(target, damage);
        }
        shootSound.Play();
        particleSystem.Play();
    }

    private void OnDrawGizmos()
    {
        // Draws the range of the tower around it
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public float GetCost()
    {
        return cost;
    }
}
