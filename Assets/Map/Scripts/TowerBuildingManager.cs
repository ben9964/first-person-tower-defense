// This script acts as a manager for building towers.
// It provides a singleton reference and methods to get the tower to be built.
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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

    // Initialization method called once at the start.
    void Start()
    {
        BuildTower = Tower;
    }

    // Private variable to store the specific tower to be built.
    private GameObject BuildTower;

    // Method to return the tower to be built.
    public GameObject GetBuildTower()
    {
        return BuildTower;
    }
}
