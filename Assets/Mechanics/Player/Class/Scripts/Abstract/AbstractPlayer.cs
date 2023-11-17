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
    public Camera cameraPrefab;
    public FirstPersonController controller;
    public Color32 colour;
    public String name;
    public float startingMoney;
    public float startingXP;
    private Camera _camera;
    private HudManager _hudManager;
    private AbstractWeapon _currentWeapon;
	private float money;
    private float XP;
    private bool isFrozen;
    protected override void Awake()
    {
        base.Awake();
        Global.SetPlayer(this);
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
        money = startingMoney;
        XP = startingXP;
        ToggleCursorLock(true);
        _hudManager.GetHealthBar().SetHealthPercentage(GetHealth()/GetMaxHealth());
        _hudManager.SetMoney(money);
        _hudManager.SetXP(XP);
        Global.GetWaveSpawner().CheckWaveFinished(false);
    }

    public void ToggleCursorLock(bool lockState)
    {
        if (lockState)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }else{
            Cursor.lockState = CursorLockMode.None;
        }
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
        if (IsDead() || _hudManager.IsPaused()) return;
        _currentWeapon.Use();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        _hudManager.GetHealthBar().SetHealthPercentage(GetHealth()/GetMaxHealth());
    }

    protected override void _die()
    {
        _hudManager.SendMessage("You Died!", new Color32(255, 0, 0, 255));
        Freeze();
    }

    public void Freeze()
    {
        Freeze(false);
    }
    
    public void Freeze(bool preserve)
    {
        Time.timeScale = 0;
        controller.RotationSpeed = 0;
        ToggleCursorLock(false);
        if (!preserve)
        {
            isFrozen = true;
        }
    }

    public void UnFreeze()
    {
        UnFreeze(false);
    }
    
    public void UnFreeze(bool preserve)
    {
        Time.timeScale = 1;
        controller.RotationSpeed = 1;
        ToggleCursorLock(true);
        if (!preserve)
        {
            isFrozen = false; 
        }
    }

    public Color32 GetColour()
    {
        return colour;
    }

    public bool IsFrozen()
    {
        return isFrozen;
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

    public HudManager GetHud()
    {
        return _hudManager;
    }

    public float GetMoney()
    {
        return money;
    }
    public void AddMoney(float amount)
    {
        money += amount;
        _hudManager.SetMoney(money);
    }

    public void RemoveMoney(float amount)
    {
        money -= amount;
        _hudManager.SetMoney(money);
    }

    public bool HasMoney(float amount)
    {
        return money >= amount;
    }

     public float GetXP()
    {
        return XP;
    }
    public void AddXP(float amount)
    {
        XP += amount;
        _hudManager.SetXP(XP);
        Debug.Log(XP);
    }
}
