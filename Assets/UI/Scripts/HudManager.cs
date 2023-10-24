using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{

    private static HudManager Instance { get; set; }
    
    public TextMeshProUGUI playerMessageObj;
    public Slider healthBar;

    void Start()
    {
        Instance = this;
    }

    //generic method for sending the player messages
    public static void SendMessage(String message, Color32 colour, float removeAfter)
    {
        SendMessage(message, colour);

        //remove the text from screen after x seconds
        Instance.Invoke(() =>
        {
            Instance.playerMessageObj.gameObject.SetActive(false);
        }, removeAfter);
    }
    
    public static void SendMessage(String message, Color32 colour)
    {
        Instance.playerMessageObj.SetText(message);
        Instance.playerMessageObj.color = colour;
        Instance.playerMessageObj.gameObject.SetActive(true);
    }

    public static void clearMessage()
    {
        Instance.playerMessageObj.gameObject.SetActive(false);
    }

    public static void setHealthPercentage(float value)
    {
        if (value > 1)
        {
            value = 1.0f;
        }
        else if (value < 0)
        {
            value = 0.0f;
        }
        Instance.healthBar.value = value;
    }
    
}
