// This script acts as a manager for building towers.
// It provides a singleton reference and methods to get the tower to be built.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerBuildingManager : MonoBehaviour
{
    // Singleton instance of the TowerBuildingManager.
    public static TowerBuildingManager instance;
    
    private Dictionary<String, GameObject> towers = new(){};

    private String _currentSelectedTower;
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple TowerBuildingManagers in Scene");
        }
        instance = this;
    }

    public String GetCurrentlySelectedTower()
    {
        return _currentSelectedTower;
    }
    
    public void SetCurrentlySelectedTower(String tower)
    {
        _currentSelectedTower = tower;
    }
    
    public GameObject GetBuildTower(String tower)
    {
        return towers[tower];
    }

    private void OnRightClick(InputValue value)
    {
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

        GameObject towerToBuild = GetBuildTower(_currentSelectedTower);
        Tower tower = towerToBuild.GetComponent<Tower>();
        if (Global.HasPlayer())
        {
            AbstractPlayer player = GameObject.FindWithTag("Player").GetComponent<AbstractPlayer>();
        
            if (!player.HasMoney(tower.GetCost()))
            {
                player.GetHud().SendMessage("You need $50 to place this", new Color32(255, 0, 0, 255), 2);
                return;
            }
            player.RemoveMoney(tower.GetCost());
        }
        
        GameObject liveTower = Instantiate(towerToBuild, target, Quaternion.identity);
        liveTower.transform.LookAt(Global.GetPlayer().transform);
    }
}
