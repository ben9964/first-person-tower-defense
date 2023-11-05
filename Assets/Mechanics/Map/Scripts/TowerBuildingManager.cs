// This script acts as a manager for building towers.
// It provides a singleton reference and methods to get the tower to be built.
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerBuildingManager : MonoBehaviour
{
    // Singleton instance of the TowerBuildingManager.
    public static TowerBuildingManager instance;
    
    // Ensure only one instance of TowerBuildingManager exists in the scene.
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple TowerBuildingManagers in Scene");
        }
        instance = this;
    }

    // Public variable to reference the tower prefab.
    public GameObject Tower;

    // Method to return the tower to be built.
    public GameObject GetBuildTower()
    {
        return Tower;
    }

    private void OnRightClick(InputValue value)
    {
        Debug.Log("right clicked");
        Ray ray = Global.GetPlayer().GetCamera().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitData;

        Vector3 target;
        if (Physics.Raycast(ray, out hitData, 10))
        {
            target = hitData.point;
        }
        else
        {
            return;
        }
        
        TowerController tower = Tower.GetComponent<TowerController>();
        if (Global.HasPlayer())
        {
            AbstractPlayer player = GameObject.FindWithTag("Player").GetComponent<AbstractPlayer>();
        
            if (!player.HasMoney(tower.GetCost())) return;
            player.RemoveMoney(tower.GetCost());
        }
        
        Instantiate(Tower, target, transform.rotation);
    }
}
