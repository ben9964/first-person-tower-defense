using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Characters : MonoBehaviour
{
    
    public static Characters Instance { get; private set; }
    
    public GameObject[] characterPrefabs;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }


    public static GameObject GetCharacter(int index)
    {
        return Instance.characterPrefabs[index];
    }

    public static int GetAmount()
    {
        return Instance.characterPrefabs.Length;
    }

    public static GameObject[] GetAll()
    {
        return Instance.characterPrefabs;
    }
}
