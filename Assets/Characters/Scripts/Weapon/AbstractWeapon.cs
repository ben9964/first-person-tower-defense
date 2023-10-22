using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    public float damage;
    public float projectileSpeed;
    public Transform muzzle;
    public GameObject projectilePrefab;
    public abstract void Use();

    public float GetDamage()
    {
        return damage;
    }

    public float GetProjectileSpeed()
    {
        return projectileSpeed;
    }

    public Transform GetMuzzle()
    {
        return this.muzzle;
    }
}
