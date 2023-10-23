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
                this.transform.parent).GetComponent<Camera>();
            InitWeapon();
        }
        
    }

    private void Start()
    {
        InitPlayerDefaults();
    }

    private void InitPlayerDefaults()
    {
        this._health = maxHealth;
        var controller = this.gameObject.GetComponentInParent<FirstPersonController>(false);
        if (controller != null)
        {
            controller.MoveSpeed = speed;
        }
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
        if (!IsInGame()) return;
        _currentWeapon.Use();
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

    public Camera GetCamera()
    {
        return this._camera;
    }
}
