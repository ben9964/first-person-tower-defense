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
    public Slider healthBar;
    public TextMeshProUGUI moneyText;
    public String moneyString;
    public TextMeshProUGUI waveSpawnText;

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

    public void setHealthPercentage(float value)
    {
        if (value > 1)
        {
            value = 1.0f;
        }
        else if (value < 0)
        {
            value = 0.0f;
        }
        healthBar.value = value;
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
    
}
