using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : LivingEntity
{
    private Transform _target;
    public float attackDamage;
    public Slider healthBar;
    public float moneyReward;
    public float XPReward;

    private NavMeshAgent agent;
    private String currentTargetTag = "End";

    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        _target = GameObject.FindWithTag(currentTargetTag).transform;
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        
        if (waypoints.Length == 1)
        {
            float roll = Random.Range(0f, 1f);
            if (roll < 0.5f)
            {
                currentTargetTag = "Waypoint";
                _target = GameObject.FindWithTag(currentTargetTag).transform;
            }
        }
        else if (waypoints.Length > 1)
        {
            currentTargetTag = "Waypoint";
            _target = waypoints[Random.Range(0, waypoints.Length)].transform;
        }
        agent.destination = _target.position;
        
        UpdateHealthBar();
    }

    void Update ()
    {
        if (currentTargetTag == "Waypoint" && Vector3.Distance(transform.position, GameObject.FindWithTag("Waypoint").transform.position) < 2f)
        {
            currentTargetTag = "End";
            _target = GameObject.FindWithTag(currentTargetTag).transform;
            agent.destination = _target.position;
        }
        //TODO: Ai to attack player - Yusuf
    }

    protected override void _die()
    {
        //TODO: maybe some cool particle explosion idk
        Destroy(gameObject);
        if (Global.HasPlayer())
        {
            Global.GetPlayer().AddMoney(moneyReward);
            Global.GetPlayer().AddXp(XPReward);
            Global.GetWaveSpawner().decreaseEnemiesAlive();
            Global.GetWaveSpawner().CheckWaveFinished(); 
        }
    }

    public float GetAttackDamage()
    {
        return attackDamage;
    }

    public virtual void Attack(AbstractPlayer player)
    {
        player.TakeDamage(attackDamage);
    }

    public NavMeshAgent GetAgent()
    {
        return this.agent;
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

    public float GetMoneyReward()
    {
        return moneyReward;
    }

    public float GetXPReward()
    {
        return XPReward;
    }
}
