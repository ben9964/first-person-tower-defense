﻿using Unity.Mathematics;
using UnityEngine;

public class Pistol : AbstractWeapon
    {
        public override void Use()
        {
            GameObject bullet = Instantiate(this.projectilePrefab, GetMuzzle().position, quaternion.identity);
            AbstractProjectile projectile = bullet.GetComponent<AbstractProjectile>();
            projectile.SetShooter(GameObject.FindWithTag("Player").GetComponentInChildren<AbstractPlayer>());
            projectile.SetWeapon(this);
        }
    }