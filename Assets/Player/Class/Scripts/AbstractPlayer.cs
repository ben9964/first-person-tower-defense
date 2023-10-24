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
    public GameObject weaponPrefab;
    private AbstractWeapon _currentWeapon;
    public Camera cameraPrefab;
    private Camera _camera;

    private void Awake()
    {
        if (IsInGame())
        {
            _camera = Instantiate(cameraPrefab, cameraPrefab.transform.position, cameraPrefab.transform.rotation,
                transform.parent).GetComponent<Camera>();
            InitWeapon();
        }
        
    }

    private void Start()
    {
        InitPlayerDefaults();
    }

    private void InitPlayerDefaults()
    {
        _health = maxHealth;
        var controller = gameObject.GetComponentInParent<FirstPersonController>(false);
        if (controller != null)
        {
            controller.MoveSpeed = speed;
        }
        HudManager.setHealthPercentage(_health/maxHealth);
    }

    private void InitWeapon()
    {
        GameObject weapon = Instantiate(weaponPrefab, _camera.transform.Find("HandPos").position, weaponPrefab.transform.rotation,
            _camera.transform.Find("HandPos"));
        _currentWeapon = weapon.GetComponent<AbstractWeapon>();
    }

    private static bool IsInGame()
    {
        return !SceneManager.GetActiveScene().name.Equals("CharacterSelect");
    }

    public void OnLeftClick(InputValue value)
    {
        if (!IsInGame() || IsDead()) return;
        _currentWeapon.Use();
    }

    public bool IsDead()
    {
        return _health <= 0;
    }

    public void TakeDamage(float amount)
    {
        _health -= amount;
        HudManager.setHealthPercentage(_health/maxHealth);
        if (_health <= 0)
        {
            _die();
        }
    }

    private void _die()
    {
        HudManager.SendMessage("You Died!", new Color32(255, 0, 0, 255));
        Time.timeScale = 0;
        gameObject.GetComponentInParent<FirstPersonController>().RotationSpeed = 0;
    }

    public float GetHealth()
    {
        return _health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetSpeed()
    {
        return this.speed;
    }

    public Camera GetCamera()
    {
        return this._camera;
    }
}
