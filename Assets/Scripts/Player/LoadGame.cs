using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public Transform spawnPos;
    public Vector3 offset;
    public GameObject defaultCharacter;
    public GameObject mapManagerPrefab;

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

        if (GameSavingManager.instance.GetGameData() != null)
        {
            GameSavingManager.instance.LoadToGame();
        }

        GameObject manager = Instantiate(mapManagerPrefab, mapManagerPrefab.transform.position, mapManagerPrefab.transform.rotation);
        Global.SetWaveSpawner(manager.GetComponent<WaveSpawner>());
        Global.SetMap(SceneManager.GetActiveScene().name);
    }
}
