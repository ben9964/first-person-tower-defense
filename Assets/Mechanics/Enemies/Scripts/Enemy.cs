using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : LivingEntity
{
    private Transform _target;
    private int _waypointIndex = 0;
    public float attackDamage;
    public Slider healthBar;

    void Start () {
        _target = Waypoints.points[0];
        UpdateHealthBar();
    }

    void Update () {
        Vector3 dir = _target.position - transform.position;
        transform.Translate(Time.deltaTime * GetSpeed() * dir.normalized , Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.2f) {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint() {
        if (_waypointIndex >= Waypoints.points.Length - 1) {
            Destroy(gameObject);
            return;
        }

        _waypointIndex++;
        _target = Waypoints.points[_waypointIndex];
    }

    protected override void _die()
    {
        //TODO: maybe some cool particle explosion idk
        
        Destroy(gameObject);
    }

    public float GetAttackDamage()
    {
        return attackDamage;
    }

    public virtual void Attack(AbstractPlayer player)
    {
        player.TakeDamage(attackDamage);
    }
    
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        UpdateHealthBar();
    }

    protected void UpdateHealthBar()
    {
        healthBar.value = GetHealth()/GetMaxHealth();
    }
}
