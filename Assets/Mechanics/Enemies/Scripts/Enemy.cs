using System;
using System.Collections;
using System.Collections.Generic;
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
        
        //generate random roll between 0 and 1
        float roll = Random.Range(0f, 1f);
        if (roll < 0.5f && GameObject.FindWithTag("Waypoint") != null)
        {
            currentTargetTag = "Waypoint";
        }
        agent.destination = GameObject.FindWithTag(currentTargetTag).transform.position;
        
        UpdateHealthBar();
    }

    void Update ()
    {
        if (currentTargetTag == "Waypoint" && Vector3.Distance(transform.position, GameObject.FindWithTag("Waypoint").transform.position) < 2f)
        {
            currentTargetTag = "End";
            agent.destination = GameObject.FindWithTag(currentTargetTag).transform.position;
        }
        //TODO: Ai to attack player - Yusuf
    }

    protected override void _die()
    {
        //TODO: maybe some cool particle explosion idk
        Destroy(gameObject);
        if (Global.HasPlayer())
        {
            GameObject.FindWithTag("Player").GetComponent<AbstractPlayer>().AddMoney(moneyReward);
            GameObject.FindWithTag("Player").GetComponent<AbstractPlayer>().AddXp(XPReward);
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
