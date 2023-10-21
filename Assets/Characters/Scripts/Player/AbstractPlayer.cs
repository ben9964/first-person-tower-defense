using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public abstract class AbstractPlayer : MonoBehaviour
{
    private float _health;
    public float maxHealth;
    public float speed;
    public GameObject currentWeapon;

    private void Start()
    {
        this._health = maxHealth;
        var controller = this.gameObject.GetComponentInParent<FirstPersonController>(false);
        if (controller != null)
        {
            controller.MoveSpeed = speed;
        }
    }

    private static bool IsInGame()
    {
        return !SceneManager.GetActiveScene().name.Equals("CharacterSelect");
    }

    public void OnLeftClick(InputValue value)
    {
        Debug.Log("Left Click");
        currentWeapon.GetComponent<AbstractWeapon>().Use();
    }

    public void TakeDamage(float amount)
    {
        this._health -= amount;
        if (this._health <= 0)
        {
            _die();
        }
    }

    private void _die()
    {
        GameGraphics.ShowDeathMessage();
        Time.timeScale = 0;
        gameObject.GetComponentInParent<FirstPersonController>().RotationSpeed = 0;
    }

    public float GetHealth()
    {
        return this._health;
    }

    public float GetMaxHealth()
    {
        return this.maxHealth;
    }

    public float GetSpeed()
    {
        return this.speed;
    }
}
