// This script handles the logic related to the base's health and the lose state in the game. 
// When the base's health reaches zero or below, the player loses the game.

using System;
using System.Collections;              
using System.Collections.Generic;      
using UnityEngine;                   
using TMPro;
using UnityEngine.Serialization;

public class LoseState : MonoBehaviour
{
    public float maxBaseHealth = 100.0f;
    public float baseHealth;      // Declare a public integer for the base's initial health, set to 100 by default.

    // This method is called once at the beginning of the game.
    void Start()
    {
        baseHealth = maxBaseHealth;
        UpdateBaseHealthBar();         // Initialize the health display when the game starts.
    }

    // This function decreases the base's health by a given damage amount and updates the UI.
    public void DecreaseBaseHealth(int damageAmount)
    {
        baseHealth -= damageAmount;   // Deduct the damage amount from the base's health.

        UpdateBaseHealthBar();         // Update the UI to reflect the new health value.

        // If health has reached zero or gone below, trigger the lose state.
        if(baseHealth <= 0)
        {
            lose();
        }
    }

    // This function updates the text UI element to display the current base health.
    private void UpdateBaseHealthBar()
    {
        baseHealth = Math.Max(baseHealth, 0);
        Global.GetPlayer().GetHud().GetBaseHealthBar().SetHealthPercentage(baseHealth/maxBaseHealth);
    }

    // This function handles the lose state of the game.
    private void lose()
    {
        AbstractPlayer player = Global.GetPlayer(); // Find the player game object.
        player.GetHud().SendMessage("You Lose!", Color.red);                                    // Send a message to the player's HUD indicating the loss.
        player.Freeze();                                                                        // Freeze the player's actions, presumably ending their ability to play.
    }
}
