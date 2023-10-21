using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractProjectile : MonoBehaviour
{
    private AbstractPlayer _shooter;
    private AbstractWeapon _weaponThatShot;
    void Update()
    {
        this.transform.Translate(_weaponThatShot.GetProjectileSpeed() * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //TODO: Add damaging enemy logic
            
            Destroy(gameObject);
        }
    }

    public void SetShooter(AbstractPlayer shooter)
    {
        _shooter = shooter;
    }

    public void SetWeapon(AbstractWeapon weapon)
    {
        _weaponThatShot = weapon;
    }
}
