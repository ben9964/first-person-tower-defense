using Unity.Mathematics;
using UnityEngine;

public class Pistol : AbstractWeapon
    {
        public override void Use()
        {
            GameObject bullet = Instantiate(this.projectileObject, this.gameObject.transform.position, quaternion.identity, this.transform);
            AbstractProjectile projectile = bullet.GetComponent<AbstractProjectile>();
            projectile.SetShooter(GameObject.FindWithTag("Player").GetComponent<AbstractPlayer>());
            projectile.SetWeapon(this);
        }
    }
