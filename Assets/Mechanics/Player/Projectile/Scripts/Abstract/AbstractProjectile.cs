using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AbstractProjectile : Entity
{
    protected AbstractPlayer _shooter;
    protected AbstractWeapon _weaponThatShot;
    public float aliveTime;

    private void Awake()
    {
        Destroy(gameObject, aliveTime);
    }

    void Start()
    {
        SpawnProjectile();
    }

    protected virtual void SpawnProjectile()
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
        GetComponent<Rigidbody>().AddForce(direction.normalized * GetSpeed(), ForceMode.Force);
    }

    private void OnCollisionEnter(Collision other)
    {
        HandleCollision(other);
    }

    protected virtual void HandleCollision(Collision other)
    {
        Destroy(gameObject);
        if (other.gameObject.CompareTag("Enemy"))
        {
            LivingEntity enemy = other.gameObject.GetComponent<LivingEntity>();
            enemy.TakeDamage(_weaponThatShot.GetDamage());
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
