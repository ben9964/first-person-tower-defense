using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    public Transform spawnPos;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("character");
        GameObject character = Characters.Instance.GetCharacter(selectedCharacter);
        GameObject clone = Instantiate(character, spawnPos.position, Quaternion.identity);
    }
}
