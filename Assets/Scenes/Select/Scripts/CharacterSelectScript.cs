using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class CharacterSelectScript : MonoBehaviour
{
    public int selectedCharacter;
    public int selectedMap;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI mapName;

    private List<GameObject> characterCache = new List<GameObject>();
    public String[] maps;

    public void Start()
    {
        InitCharacters();
        InitMaps();
        AbstractPlayer playerPrefabComponent = CharacterDB.GetCharacter(selectedCharacter).GetComponent<AbstractPlayer>();
        characterName.SetText(playerPrefabComponent.GetName());
        characterName.color = playerPrefabComponent.GetColour();
        characterCache[selectedCharacter].SetActive(true);
    }

    public void SelectNextCharacter()
    {
        characterCache[selectedCharacter].SetActive(false);
        selectedCharacter += 1;
        if (selectedCharacter >= CharacterDB.GetAmount())
        {
            selectedCharacter = 0;
        }

        AbstractPlayer playerPrefabComponent = CharacterDB.GetCharacter(selectedCharacter).GetComponent<AbstractPlayer>();
        characterName.SetText(playerPrefabComponent.GetName());
        characterName.color = playerPrefabComponent.GetColour();
        characterCache[selectedCharacter].SetActive(true);
    }

    public void SelectPreviousCharacter()
    {
        characterCache[selectedCharacter].SetActive(false);
        selectedCharacter -= 1;
        if (selectedCharacter < 0)
        {
            selectedCharacter = CharacterDB.GetAmount() - 1;
        }

        AbstractPlayer playerPrefabComponent = CharacterDB.GetCharacter(selectedCharacter).GetComponent<AbstractPlayer>();
        characterName.SetText(playerPrefabComponent.GetName());
        characterName.color = playerPrefabComponent.GetColour();
        characterCache[selectedCharacter].SetActive(true);
    }
    
    public void SelectNextMap()
    {
        selectedMap ++;
        if (selectedMap >= maps.Length)
        {
            selectedMap = 0;
        }
        
        mapName.SetText(maps[selectedMap]);
    }

    public void SelectPreviousMap()
    {
        selectedMap --;
        if (selectedMap < 0)
        {
            selectedMap = maps.Length - 1;
        }
        
        mapName.SetText(maps[selectedMap]);
    }

    public void StartButtonPress()
    {
        CharacterDB.SetSelected(selectedCharacter);
        Util.LoadScene(maps[selectedMap]);
    }
    
    private void InitCharacters()
    {
        foreach (GameObject prefab in CharacterDB.GetAll())
        {
            GameObject characterModel = Instantiate(prefab.transform.Find("PlayerCapsule/Model").gameObject,
                new Vector3(0, 0, 0), Quaternion.identity, GameObject.FindWithTag("CharacterRoot").transform);
            characterModel.SetActive(false);
            characterCache.Add(characterModel);
        }
    }
    
    private void InitMaps()
    {
        mapName.SetText(maps[selectedMap]);
    }
}

