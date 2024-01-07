using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    
    // References to buttons on the start menu for initiating a new game or continuing an existing game
    [Header ("Start Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    
    [SerializeField] private String levelString;
    [SerializeField] private String XPString;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private TextMeshProUGUI playerXPText;

    
    // The New Game and Continue Game button must be dragged and dropped on to the Start menu script
    //
    // This method is the start method
    // Makes the Continue Game button not interactble if there isnt a game data file
    private void Start()
    {
        if(!GameSavingManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }

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

    // This method is for when a new change is created 
    // Needs to be linked to the Start button on the start menu
    
    public void OnNewGameClicked()
    {
        DisableStartMenuButtons();
        GameSavingManager.instance.NewGameData();
        SceneManager.LoadSceneAsync("CharacterSelect");
    }

    // This method is for when the user wants to continue there game and use the data that is already saved
    // Needs to be linked to a Continue button on the start menu
    
    public void OnContinueGameClicked()
    {
        DisableStartMenuButtons();
        SceneManager.LoadSceneAsync("CharacterSelect");
    }

    public void DisableStartMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}
 