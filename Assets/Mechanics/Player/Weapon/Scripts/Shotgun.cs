using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Shotgun : AbstractWeapon
{
    public int amountOfBullets;
    public override void Use()
    {
        if (!canShoot) return;
        
        for (int i = 0; i < amountOfBullets; i++)
        {
            GameObject projectileObj = Instantiate(this.projectilePrefab, GetMuzzle().position, Quaternion.identity);
            AbstractProjectile clazz = projectileObj.GetComponent<AbstractProjectile>();
            clazz.SetShooter(Global.GetPlayer());
            clazz.SetWeapon(this);
        }
        canShoot = false;
        this.Invoke(() =>
        {
            canShoot = true;
        }, shootCooldown);
    }
}
