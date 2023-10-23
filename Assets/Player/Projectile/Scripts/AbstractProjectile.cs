using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractProjectile : MonoBehaviour
{
    private AbstractPlayer _shooter;
    private AbstractWeapon _weaponThatShot;
    public float aliveTime;

    private void Awake()
    {
        Destroy(gameObject, aliveTime);
    }

    void Start()
    {
        GameObject playerObj = this._shooter.gameObject;

        Ray ray = _shooter.GetCamera().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitData;

        Vector3 target;
        if (Physics.Raycast(ray, out hitData))
        {
            target = hitData.point;
        }
        else
        {
            target = ray.GetPoint(50);
        }

        Vector3 direction = target - _weaponThatShot.GetMuzzle().position;

        transform.forward = direction.normalized;
        GetComponent<Rigidbody>().AddForce(direction.normalized * _weaponThatShot.GetProjectileSpeed(), ForceMode.Impulse);
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