using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : LivingEntity
{
    private Transform _target;
    private int _waypointIndex = 0;
    public float attackDamage;
    public Slider healthBar;
    public float moneyReward;
    public float XPReward;

    private NavMeshAgent agent;

    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = GameObject.FindWithTag("End").transform.position;
        UpdateHealthBar();
    }

    void Update ()
    {
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
            Global.GetWaveSpawner().CheckWaveFinished(true); 
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
