using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    public float damage;
    public Transform muzzle;
    public GameObject projectilePrefab;
    public float shootCooldown;
    protected bool canShoot = true;

    public virtual void Use()
    {
        if (!canShoot) return;
        
        GameObject projectileObj = Instantiate(this.projectilePrefab, GetMuzzle().position, Quaternion.identity);
        AbstractProjectile clazz = projectileObj.GetComponent<AbstractProjectile>();
        clazz.SetShooter(GameObject.FindWithTag("Player").GetComponentInChildren<AbstractPlayer>());
        clazz.SetWeapon(this);
        canShoot = false;
        this.Invoke(() =>
        {
            canShoot = true;
        }, shootCooldown);
    }

    public float GetDamage()
    {
        return damage;
    }

    public Transform GetMuzzle()
    {
        return this.muzzle;
    }

    public void ResetShot()
    {
        canShoot = true;
    }
}
