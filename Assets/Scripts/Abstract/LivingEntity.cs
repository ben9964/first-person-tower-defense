using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : Entity
{
    public float maxHealth;
    protected float _health;
    
    private bool _invincible = false;
    private bool isDead = false;
    public float damageGracePeriod;

    protected virtual void Awake()
    {
        this._health = maxHealth;
    }

    public float GetHealth()
    {
        return _health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        if (_invincible) return;
        
        
        _health -= amount;
        if (_health <= 0 && !isDead)
        {
            isDead = true;
            _die();
        }
        else
        {
            if (damageGracePeriod <= 0) return;
            _invincible = true;
            this.Invoke(() =>
            {
                _invincible = false;
            }, damageGracePeriod);
        }
    }

    protected abstract void _die();

    public bool IsDead()
    {
        return _health <= 0;
    }
    
    public void Heal(float amount)
    {
        _health += amount;
        if (_health > maxHealth)
        {
            _health = maxHealth;
        }
    }

    public void SetMaxHealth(float amount)
    {
        this.maxHealth = amount;
    }
    
    public virtual void SetHealth(float amount)
    {
        this._health = amount;
    }
}
