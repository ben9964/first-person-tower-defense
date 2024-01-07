using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsDisplay : MonoBehaviour
{
    [SerializeField] private String levelString;
    [SerializeField] private String XPString;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private TextMeshProUGUI playerXPText;
    
    void Start()
    {
        GameData savedData = GameSavingManager.instance.GetGameData();
        if (savedData == null)
        {
            playerLevelText.gameObject.SetActive(false);
            playerXPText.gameObject.SetActive(false);
            return;
        }
        playerLevelText.SetText(levelString + savedData.GetLevel());
        playerXPText.SetText(XPString + savedData.GetXp());
    }
}
