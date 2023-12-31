using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterDB : MonoBehaviour
{
    
    public static CharacterDB Instance { get; private set; }
    
    public GameObject[] characterPrefabs;

    private int selectedCharacter = -1;

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

    public static void SetSelected(int selected)
    {
        Instance.selectedCharacter = selected;
    }

    public static int GetSelected()
    {
        return Instance.selectedCharacter;
    }
}
