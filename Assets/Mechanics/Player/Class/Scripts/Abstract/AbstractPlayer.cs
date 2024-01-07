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
    
    private Camera _camera;
    private HudManager _hudManager;
    private AbstractWeapon _currentWeapon;
	private float _money;
    private float _xp;
    private float _levelupThreshold = 100;
    private int _playerLevel;
    private bool _isFrozen;
    public float abilityCooldown;
    protected bool _canUseAbility = true;
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

    public virtual void UseAbility()
    {
        _canUseAbility = false;
        this.Invoke(() =>
        {
            this._canUseAbility = true;
        }, abilityCooldown);
    }

    private void InitPlayerDefaults()
    {
        _money = startingMoney;
        controller.MoveSpeed = speed;
        ToggleCursorLock(true);
        _hudManager.GetHealthBar().SetHealthPercentage(GetHealth()/GetMaxHealth());
        _hudManager.SetMoney(_money);
        _hudManager.SetXP(_xp);
        _hudManager.SetLevel(_playerLevel);
        Global.GetWaveSpawner().CheckWaveFinished();
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
        if (IsDead() || _hudManager.IsPaused() || _hudManager.IsInBuyMenu() || Global.IsInMapView()) return;
        _currentWeapon.Use();
    }
    
    public AbstractWeapon GetCurrentWeapon()
    {
        return _currentWeapon;
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
    public float GeLevel()
    {
        return _xp;
    }

    public void AddXp(float amount)
    {
        _xp += amount;

        // Check if the XP exceeds the threshold after addition
        if (_xp >= _levelupThreshold)
        {
            LevelUp();
        }
        else
        {
            _hudManager.SetXP(_xp); // Update the XP display if not leveling up
        }
    }

    private void LevelUp()
    {
        _playerLevel++;
        _xp -= _levelupThreshold; // Reset XP accounting for overflow
        _hudManager.SetLevel(_playerLevel); // Update the level display
        _hudManager.SetXP(_xp);// Update the XP display
        _levelupThreshold *= 1.5f; // Increase the XP required for the next level

        // Implement additional level-up effects here (e.g., increase health, damage, etc.)
        
    }

    public void LoadGameData(GameData data)
    {
        this._xp = data.playerXp;
        this._playerLevel = data.playerLevel;
    }

    public void SaveGameData(ref GameData data)
    {
        data.playerLevel = this._playerLevel;
        data.playerXp = this._xp;
    }
}
