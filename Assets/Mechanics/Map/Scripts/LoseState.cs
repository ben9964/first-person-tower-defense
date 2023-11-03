// This script handles the logic related to the base's health and the lose state in the game. 
// When the base's health reaches zero or below, the player loses the game.

using System.Collections;              
using System.Collections.Generic;      
using UnityEngine;                   
using TMPro;                         

public class LoseState : MonoBehaviour 
{
    public int Base_Health = 100;      // Declare a public integer for the base's initial health, set to 100 by default.
    public TextMeshProUGUI baseHealth; // Declare a public UI text element to display the base's health.

    // This method is called once at the beginning of the game.
    void Start()
    {
        UpdateBaseHealthBar();         // Initialize the health display when the game starts.
    }

    // This function decreases the base's health by a given damage amount and updates the UI.
    public void DecreaseBaseHealth(int damageAmount)
    {
        Base_Health -= damageAmount;   // Deduct the damage amount from the base's health.

        if(Base_Health < 0)            // Check if the health has gone below zero.
        {
            Base_Health = 0;           // If it has, set the health to zero to avoid negative values.
        }

        UpdateBaseHealthBar();         // Update the UI to reflect the new health value.

        // If health has reached zero or gone below, trigger the lose state.
        if(Base_Health <= 0)
        {
            lose();
        }
    }

    // This function updates the text UI element to display the current base health.
    private void UpdateBaseHealthBar()
    {
        baseHealth.text = "Base Health: " + Base_Health; // Set the UI text to show the base's current health.
    }

    // This function handles the lose state of the game.
    private void lose()
    {
        AbstractPlayer player = GameObject.FindWithTag("Player").GetComponent<AbstractPlayer>(); // Find the player game object.
        player.GetHud().SendMessage("You Lose!", Color.red);                                    // Send a message to the player's HUD indicating the loss.
        player.Freeze();                                                                        // Freeze the player's actions, presumably ending their ability to play.
    }
}
