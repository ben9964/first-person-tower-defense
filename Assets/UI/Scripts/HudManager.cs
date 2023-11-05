using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    
    public TextMeshProUGUI playerMessageObj;
    public HealthBar healthBar;
    public HealthBar baseHealthBar;
    public TextMeshProUGUI moneyText;
    public String moneyString;
    public TextMeshProUGUI waveSpawnText;
    public GameObject escMenuPrefab;
    private GameObject _escMenu;
    private bool isPaused = false;

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

    public void clearMessage()
    {
        playerMessageObj.gameObject.SetActive(false);
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
        isPaused = true;
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
        isPaused = false;
    }

    public bool IsPaused()
    {
        return isPaused;
    }
    
}
