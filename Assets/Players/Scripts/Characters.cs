using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Characters : MonoBehaviour
{
    
    public static Characters Instance { get; private set; }
    
    public GameObject[] staticCharacters;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }


    public GameObject GetCharacter(int index)
    {
        return Instance.staticCharacters[index];
    }

    public int getAmount()
    {
        return Instance.staticCharacters.Length;
    }
}
