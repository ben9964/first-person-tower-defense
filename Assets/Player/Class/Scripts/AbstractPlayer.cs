using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public abstract class AbstractPlayer : LivingEntity
{
    public GameObject weaponPrefab;
    private AbstractWeapon _currentWeapon;
    
    public Camera cameraPrefab;
    private Camera _camera;
    
    public FirstPersonController controller;

    protected override void Awake()
    {
        base.Awake();
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
        controller.MoveSpeed = speed;
        HudManager.setHealthPercentage(GetHealth()/GetMaxHealth());
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

    public void HandleUseWeapon(InputValue value)
    {
        if (!IsInGame() || IsDead()) return;
        _currentWeapon.Use();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        HudManager.setHealthPercentage(GetHealth()/GetMaxHealth());
    }

    protected override void _die()
    {
        HudManager.SendMessage("You Died!", new Color32(255, 0, 0, 255));
        Time.timeScale = 0;
        controller.RotationSpeed = 0;
    }

    public Camera GetCamera()
    {
        return this._camera;
    }

    public virtual void HandleCollision(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Attack(this);
        }
    }
}
