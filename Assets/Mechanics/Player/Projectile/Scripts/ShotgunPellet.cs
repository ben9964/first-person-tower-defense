using UnityEngine;

public class ShotgunPellet : AbstractProjectile
{
    public float spreadRadius;
    
    protected override void SpawnProjectile()
    {
        GameObject playerObj = this._shooter.gameObject;
        
        float randomSpreadX = Random.Range(-spreadRadius, spreadRadius);
        float randomSpreadY = Random.Range(-spreadRadius, spreadRadius);

        
        Ray ray = _shooter.GetCamera().ViewportPointToRay(new Vector3(0.5f + randomSpreadX, 0.5f + randomSpreadY, 0));
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
        GetComponent<Rigidbody>().AddForce(direction.normalized * GetSpeed(), ForceMode.Impulse);
    }
    
    protected override void HandleCollision(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            LivingEntity enemy = other.gameObject.GetComponent<LivingEntity>();
            enemy.TakeDamage(_weaponThatShot.GetDamage());
        }
    }
}
