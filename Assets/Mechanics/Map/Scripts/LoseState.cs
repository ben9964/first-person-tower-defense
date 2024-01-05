// This script handles the logic related to the base's health and the lose state in the game. 
// When the base's health reaches zero or below, the player loses the game.

using System;
using System.Collections;              
using System.Collections.Generic;      
using UnityEngine;                   
using TMPro;
using UnityEngine.Serialization;

public class LoseState : MonoBehaviour, GameSavingInterface
{
    
    public float maxBaseHealth = 100.0f;
    private float _baseHealth;      // Declare a public integer for the base's initial health, set to 100 by default.
    

    // This function decreases the base's health by a given damage amount and updates the UI.
    public void DecreaseBaseHealth(int damageAmount)
    {
        _baseHealth -= damageAmount;
        UpdateBaseHealthBar();  

        // If health has reached zero or gone below, trigger the lose state.
        if(_baseHealth <= 0)
        {
            lose();
        }

        Global.GetWaveSpawner().CheckWaveFinished(false);
    }
    
    public void UpdateBaseHealthBar()
    {
        _baseHealth = Math.Max(_baseHealth, 0);
        Global.GetPlayer().GetHud().GetBaseHealthBar().SetHealthPercentage(_baseHealth/maxBaseHealth);
    }
    
    private void lose()
    {
        AbstractPlayer player = Global.GetPlayer(); 
        player.GetHud().SendMessage("You Lose!", Color.red);                                   
        player.Freeze();                                                                    
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided with end");
        Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            Debug.Log("decreasing base health");
            DecreaseBaseHealth((int)enemy.GetAttackDamage());
            Destroy(other.gameObject);
        }
        
    }

    public void LoadGameDate(GameData data)
    {
        this._baseHealth = data.baseHealth;
    }

    public void SaveGameData(ref GameData data)
    {
        data.baseHealth = this._baseHealth;
    }
}
