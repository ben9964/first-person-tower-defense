using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    public float damage;
    public Transform muzzle;
    public GameObject projectilePrefab;

    public virtual void Use()
    {
        GameObject projectileObj = Instantiate(this.projectilePrefab, GetMuzzle().position, quaternion.identity);
        AbstractProjectile clazz = projectileObj.GetComponent<AbstractProjectile>();
        clazz.SetShooter(GameObject.FindWithTag("Player").GetComponentInChildren<AbstractPlayer>());
        clazz.SetWeapon(this);
    }

    public float GetDamage()
    {
        return damage;
    }

    public Transform GetMuzzle()
    {
        return this.muzzle;
    }
}
