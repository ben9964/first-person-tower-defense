using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

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

    private void OnCollisionEnter(Collision other)
    {
        HandleCollision(other);
    }

    private void HandleCollision(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("collided with enemy");
            LivingEntity enemy = other.gameObject.GetComponent<LivingEntity>();
            Debug.Log("enemy health pre: " + enemy.GetHealth());
            enemy.TakeDamage(_weaponThatShot.GetDamage());
            Debug.Log("enemy health post: " + enemy.GetHealth());
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
