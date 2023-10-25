using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : Entity
{
    public float maxHealth;
    private float _health;

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
        _health -= amount;
        if (_health <= 0)
        {
            _die();
        }
    }

    protected abstract void _die();

    public bool IsDead()
    {
        return _health <= 0;
    }
}
