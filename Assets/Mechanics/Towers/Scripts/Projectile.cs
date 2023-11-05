using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float projSpeed = 20f;
    private Transform target;
    private float damage;

    // Method to call 
    public void SelectTarget(Transform _target, float _damage)
    {
        target = _target;
        damage = _damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * projSpeed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            LivingEntity enemy = collision.gameObject.GetComponent<LivingEntity>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
