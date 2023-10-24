using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HudManager : MonoBehaviour
{

    private static HudManager Instance { get; set; }

    public GameObject playerHudPrefab;
    private Canvas _playerHud;

    public String playerMessageObjectName;
    private TextMeshProUGUI _playerMessageObj;

    void Start()
    {
        Instance = this;

        _playerHud = Instantiate(playerHudPrefab, playerHudPrefab.transform.position,
            playerHudPrefab.transform.rotation).GetComponent<Canvas>();
        _playerMessageObj = _playerHud.transform.Find(playerMessageObjectName).GetComponent<TextMeshProUGUI>();
    }

    //generic method for sending the player messages
    public static void sendMessage(String message, Color32 colour, float removeAfter)
    {
        sendMessage(message, colour);

        //remove the text from screen after x seconds
        Instance.Invoke(() =>
        {
            Instance._playerMessageObj.gameObject.SetActive(false);
        }, removeAfter);
    }
    
    public static void sendMessage(String message, Color32 colour)
    {
        Instance._playerMessageObj.SetText(message);
        Instance._playerMessageObj.color = colour;
        Instance._playerMessageObj.gameObject.SetActive(true);
    }

    public static void clearMessage()
    {
        Instance._playerMessageObj.gameObject.SetActive(false);
    }
    
}
