using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    public float damage;
    public float projectileSpeed;
    public GameObject projectileObject;
    public abstract void Use();

    public float GetDamage()
    {
        return damage;
    }

    public float GetProjectileSpeed()
    {
        return projectileSpeed;
    }
}
