﻿using System;
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
    public Camera cameraPrefab;
    public FirstPersonController controller;
    public Color32 colour;
    public String name;
    
    private Camera _camera;
    private HudManager _hudManager;
    private AbstractWeapon _currentWeapon;

    protected override void Awake()
    {
        base.Awake();
        GameObject playerHudPrefab = Global.GetHudPrefab();
        _hudManager = Instantiate(playerHudPrefab, playerHudPrefab.transform.position,
            playerHudPrefab.transform.rotation).GetComponent<HudManager>();
        _camera = Instantiate(cameraPrefab, cameraPrefab.transform.position, cameraPrefab.transform.rotation,
            transform.parent).GetComponent<Camera>();
        InitWeapon();
    }

    private void Start()
    {
        InitPlayerDefaults();
    }

    private void InitPlayerDefaults()
    {
        controller.MoveSpeed = speed;
        ToggleCursorLock();
        _hudManager.setHealthPercentage(GetHealth()/GetMaxHealth());
    }

    public void ToggleCursorLock()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }else{
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void InitWeapon()
    {
        GameObject weapon = Instantiate(weaponPrefab, _camera.transform.Find("HandPos").position, weaponPrefab.transform.rotation,
            _camera.transform.Find("HandPos"));
        _currentWeapon = weapon.GetComponent<AbstractWeapon>();
    }

    public void HandleUseWeapon(InputValue value)
    {
        if (IsDead()) return;
        _currentWeapon.Use();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        _hudManager.setHealthPercentage(GetHealth()/GetMaxHealth());
    }

    protected override void _die()
    {
        _hudManager.SendMessage("You Died!", new Color32(255, 0, 0, 255));
        Time.timeScale = 0;
        controller.RotationSpeed = 0;
    }

    public Color32 GetColour()
    {
        return colour;
    }

    public String GetName()
    {
        return name;
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

    public void HideHud()
    {
        _hudManager.SetVisible(false);
    }

    public void ShowHud()
    {
        _hudManager.SetVisible(true);
    }
}