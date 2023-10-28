using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectScript : MonoBehaviour
{
    public int selected;
    public String sceneToLoad;
    public TextMeshProUGUI characterName;

    private List<GameObject> characterCache = new List<GameObject>();

    public void Start()
    {
        InitCharacters();
        AbstractPlayer playerPrefabComponent = CharacterDB.GetCharacter(selected).GetComponent<AbstractPlayer>();
        characterName.SetText(playerPrefabComponent.GetName());
        characterName.color = playerPrefabComponent.GetColour();
        characterCache[selected].SetActive(true);
        DontDestroyOnLoad(gameObject);
    }

    public void SelectNext()
    {
        characterCache[selected].SetActive(false);
        selected += 1;
        if (selected >= CharacterDB.GetAmount())
        {
            selected = 0;
        }

        AbstractPlayer playerPrefabComponent = CharacterDB.GetCharacter(selected).GetComponent<AbstractPlayer>();
        characterName.SetText(playerPrefabComponent.GetName());
        characterName.color = playerPrefabComponent.GetColour();
        characterCache[selected].SetActive(true);
    }

    public void SelectPrevious()
    {
        characterCache[selected].SetActive(false);
        selected -= 1;
        if (selected < 0)
        {
            selected = CharacterDB.GetAmount() - 1;
        }

        AbstractPlayer playerPrefabComponent = CharacterDB.GetCharacter(selected).GetComponent<AbstractPlayer>();
        characterName.SetText(playerPrefabComponent.GetName());
        characterName.color = playerPrefabComponent.GetColour();
        characterCache[selected].SetActive(true);
    }

    public void StartButtonPress()
    {
        CharacterDB.SetSelected(this.selected);
        Util.LoadScene(sceneToLoad);
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
}
