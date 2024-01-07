using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    // Permanent Text object to send player various messages on screen
    public TextMeshProUGUI playerMessageObj;
    
    // Player HUD Elements
    public HealthBar healthBar;
    public HealthBar baseHealthBar;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI waveText;
    public String moneyString;
    public TextMeshProUGUI XPText;
    public String XPString;
    public TextMeshProUGUI levelText;
    public String levelString;
    public TextMeshProUGUI waveSpawnText;
    public Image abilityReadyImage;
    public Image abilityNotReadyImage;
    
    // Esc Menu
    public GameObject escMenuPrefab;
    private GameObject _escMenu;
    private bool _isPaused = false;
    
    // Tower Buy Menu
    public GameObject towerBuyMenuPrefab;
    private GameObject _towerBuyMenu;
    private bool _isInBuyMenu = false;

    private void Start()
    {
        LoseState baseState = FindObjectOfType<LoseState>();
        if (baseState != null)
        {
            baseState.UpdateBaseHealthBar();

        }
    }

    //generic method for sending the player messages
    public void SendMessage(String message, Color32 colour, float removeAfter)
    {
        SendMessage(message, colour);

        //remove the text from screen after x seconds
        this.Invoke(() =>
        {
            playerMessageObj.gameObject.SetActive(false);
        }, removeAfter);
    }
    
    public void SendMessage(String message, Color32 colour)
    {
        playerMessageObj.SetText(message);
        playerMessageObj.color = colour;
        playerMessageObj.gameObject.SetActive(true);
    }

    public void UpdateWaveText(int waveNumber, int totalWaves)
    {
        waveText.SetText("Wave: " + waveNumber + "/" + totalWaves);
    }

    public void clearMessage()
    {
        playerMessageObj.gameObject.SetActive(false);
    }

    public void AbilityReady()
    {
        abilityReadyImage.gameObject.SetActive(true);
        abilityNotReadyImage.gameObject.SetActive(false);
    }
    
    public void AbilityNotReady()
    {
        abilityReadyImage.gameObject.SetActive(false);
        abilityNotReadyImage.gameObject.SetActive(true);
    }

    public HealthBar GetHealthBar()
    {
        return healthBar;
    }

    public HealthBar GetBaseHealthBar()
    {
        return baseHealthBar;
    }

    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }

    public void SetMoney(float amount)
    {
        moneyText.SetText(moneyString + amount);
    }
    

     public void SetXP(float amount)
    {
        XPText.SetText(XPString + amount);
    }

    public void SetLevel(float amount)
    {
        levelText.SetText(levelString + amount);
    }

    public void ShowWaveSpawnText()
    {
        waveSpawnText.gameObject.SetActive(true);
    }

    public void HideWaveSpawnText()
    {
        waveSpawnText.gameObject.SetActive(false);
    }

    public void OpenEscMenu()
    {
        if (_escMenu == null)
        {
            _escMenu = Instantiate(escMenuPrefab, escMenuPrefab.transform.position, escMenuPrefab.transform.rotation);
        }

        //if the player is frozen then we want to preserve this state
        AbstractPlayer player = Global.GetPlayer();
        if (!player.IsFrozen())
        {
            Global.GetPlayer().Freeze(true);
        }
        
        _escMenu.SetActive(true);
        _isPaused = true;
    }

    public void CloseEscMenu()
    {
        if (_escMenu == null)
        {
            _escMenu = Instantiate(escMenuPrefab, escMenuPrefab.transform.position, escMenuPrefab.transform.rotation);
        }

        //if the player is frozen then we want to preserve this state
        AbstractPlayer player = Global.GetPlayer();
        if (!player.IsFrozen())
        {
            Global.GetPlayer().UnFreeze(true);
        }
        
        _escMenu.SetActive(false);
        _isPaused = false;
    }
    
    public void OpenTowerBuyMenu()
    {
        if (_towerBuyMenu == null)
        {
            _towerBuyMenu = Instantiate(towerBuyMenuPrefab, towerBuyMenuPrefab.transform.position, towerBuyMenuPrefab.transform.rotation);
        }

        //if the player is frozen then we want to preserve this state
        AbstractPlayer player = Global.GetPlayer();
        if (!player.IsFrozen())
        {
            Global.GetPlayer().Freeze(true);
        }
        
        _towerBuyMenu.SetActive(true);
        _isInBuyMenu = true;
    }
    
    public void CloseTowerBuyMenu()
    {
        if (_towerBuyMenu == null)
        {
            _towerBuyMenu = Instantiate(towerBuyMenuPrefab, towerBuyMenuPrefab.transform.position, towerBuyMenuPrefab.transform.rotation);
        }

        //if the player is frozen then we want to preserve this state
        AbstractPlayer player = Global.GetPlayer();
        if (!player.IsFrozen())
        {
            Global.GetPlayer().UnFreeze(true);
        }
        
        _towerBuyMenu.SetActive(false);
        _isInBuyMenu = false;
    }
    
    public bool IsInBuyMenu()
    {
        return _isInBuyMenu;
    }

    public bool IsPaused()
    {
        return _isPaused;
    }
    
}
