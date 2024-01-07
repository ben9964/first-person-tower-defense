using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharacterSelectScript : MonoBehaviour
{
    private List<GameObject> characterCache = new List<GameObject>();
    public int selectedCharacter;
    
    public String[] maps;
    public int selectedMap;
    
    public TextMeshProUGUI characterName;
    public GameObject characterLockedIcons;
    public TextMeshProUGUI characterLockedReq;
    
    public TextMeshProUGUI mapName;
    public GameObject mapLockedIcons;
    public TextMeshProUGUI mapLockedReq;

    private Dictionary<String, Int32> mapUnlocks = new()
    {
        {"Easy", 0},
        {"Medium", 5},
        {"Hard", 10}
    };
    
    private Dictionary<String, Int32> characterUnlocks = new()
    {
        {"Damage", 0},
        {"Tank", 3},
    };

    public Button playButton;

    public void Start()
    {
        InitCharacters();
        InitMaps();
        AbstractPlayer playerPrefabComponent = CharacterDB.GetCharacter(selectedCharacter).GetComponent<AbstractPlayer>();
        characterName.SetText(playerPrefabComponent.GetName());
        characterCache[selectedCharacter].SetActive(true);
    }

    private void PlayCheck()
    {
        playButton.interactable = true;
        characterLockedIcons.SetActive(false);
        mapLockedIcons.SetActive(false);
        int level = GameSavingManager.instance.GetGameData().GetLevel();
        AbstractPlayer playerPrefabComponent = CharacterDB.GetCharacter(selectedCharacter).GetComponent<AbstractPlayer>();
        Debug.Log(playerPrefabComponent.GetName());
        if (level < characterUnlocks[playerPrefabComponent.GetName()])
        {
            playButton.interactable = false;
            characterLockedIcons.SetActive(true);
            characterLockedReq.SetText("Level " + characterUnlocks[playerPrefabComponent.GetName()]);
            characterLockedReq.color = Color.red;
        }
        
        if (level < mapUnlocks[maps[selectedMap]])
        {
            playButton.interactable = false;
            mapLockedIcons.SetActive(true);
            mapLockedReq.SetText("Level " + mapUnlocks[maps[selectedMap]]);
            mapLockedReq.color = Color.red;
        }
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
        characterCache[selectedCharacter].SetActive(true);
        PlayCheck();
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
        characterCache[selectedCharacter].SetActive(true);
        PlayCheck();
    }
    
    public void SelectNextMap()
    {
        selectedMap ++;
        if (selectedMap >= maps.Length)
        {
            selectedMap = 0;
        }
        
        mapName.SetText(maps[selectedMap]);
        PlayCheck();
    }

    public void SelectPreviousMap()
    {
        selectedMap --;
        if (selectedMap < 0)
        {
            selectedMap = maps.Length - 1;
        }
        
        mapName.SetText(maps[selectedMap]);
        PlayCheck();
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

