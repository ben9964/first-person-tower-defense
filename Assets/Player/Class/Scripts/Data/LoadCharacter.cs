using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    public Transform spawnPos;
    public Vector3 offset;
    public GameObject defaultCharacter;

    void Start()
    {
        GameObject characterPrefab = defaultCharacter;
        if (CharacterDB.GetSelected() != -1)
        {
            int selectedCharacter = CharacterDB.GetSelected();
            characterPrefab = CharacterDB.GetCharacter(selectedCharacter);
        }
        GameObject character = Instantiate(characterPrefab, spawnPos.position+offset, Quaternion.identity);
        character.SetActive(true);
    }
}
