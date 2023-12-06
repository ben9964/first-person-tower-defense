using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public abstract class AbstractPlayer : LivingEntity, GameSavingInterface
{
    public GameObject weaponPrefab;
    public Camera cameraPrefab;
    public FirstPersonController controller;
    public Color32 colour;
    public String playerName;
    public float startingMoney;
    public float startingXp;
    private Camera _camera;
    private HudManager _hudManager;
    private AbstractWeapon _currentWeapon;
	private float _money;
    private float _xp;
    private int _playerLevel;
    private bool _isFrozen;
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
        ToggleCursorLock(true);
        _hudManager.GetHealthBar().SetHealthPercentage(GetHealth()/GetMaxHealth());
        _hudManager.SetMoney(_money);
        _hudManager.SetXP(_xp);
        Global.GetWaveSpawner().CheckWaveFinished(false);
        Debug.Log("Player Postion: " + transform.position);
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
        GameObject weapon = Instantiate(weaponPrefab,
            _camera.transform.Find("HandPos"));
        _currentWeapon = weapon.GetComponent<AbstractWeapon>();
    }

    public void HandleUseWeapon(InputValue value)
    {
        if (IsDead() || _hudManager.IsPaused() || _hudManager.IsInBuyMenu()) return;
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
            _isFrozen = true;
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
            _isFrozen = false; 
        }
    }

    public Color32 GetColour()
    {
        return colour;
    }

    public bool IsFrozen()
    {
        return _isFrozen;
    }

    public String GetName()
    {
        return playerName;
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
        return _money;
    }
    public void AddMoney(float amount)
    {
        _money += amount;
        _hudManager.SetMoney(_money);
    }

    public void RemoveMoney(float amount)
    {
        _money -= amount;
        _hudManager.SetMoney(_money);
    }

    public bool HasMoney(float amount)
    {
        return _money >= amount;
    }

     public float GetXp()
    {
        return _xp;
    }
    public void AddXp(float amount)
    {
        _xp += amount;
        _hudManager.SetXP(_xp);
        Debug.Log(_xp);
    }

    public void LoadGameDate(GameData data)
    {
        this._health = data.playerHealth;
        this._xp = data.playerXp;
        this._playerLevel = data.playerLevel;
        this._money = data.playerMoney;
    }

    public void SaveGameData(ref GameData data)
    {
        data.playerHealth = this._health;
        data.playerLevel = this._playerLevel;
        data.playerXp = this._xp;
        data.playerMoney = this._money;
    }
}
