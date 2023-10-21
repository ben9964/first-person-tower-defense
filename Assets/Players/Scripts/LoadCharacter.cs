using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    public Transform spawnPos;
    public Vector3 offset;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("character");
        GameObject characterPrefab = Characters.GetCharacter(selectedCharacter);
        Instantiate(characterPrefab, spawnPos.position+offset, Quaternion.identity, GameObject.FindWithTag("Player").transform);
    }
}
