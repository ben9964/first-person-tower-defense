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
    private HudManager hudManager;

    protected override void Awake()
    {
        base.Awake();
        GameObject playerHudPrefab = Global.GetHudPrefab();
        hudManager = Instantiate(playerHudPrefab, playerHudPrefab.transform.position,
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
        hudManager.setHealthPercentage(GetHealth()/GetMaxHealth());
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
        hudManager.setHealthPercentage(GetHealth()/GetMaxHealth());
    }

    protected override void _die()
    {
        hudManager.SendMessage("You Died!", new Color32(255, 0, 0, 255));
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

    public void HideHud()
    {
        hudManager.SetVisible(false);
    }

    public void ShowHud()
    {
        hudManager.SetVisible(true);
    }
}
