using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectScript : MonoBehaviour
{
    public int selected;

    private List<GameObject> characterCache = new List<GameObject>();

    public void Start()
    {
        InitCharacters();
        characterCache[selected].SetActive(true);
    }

    public void SelectNext()
    {
        characterCache[selected].SetActive(false);
        selected += 1;
        if (selected >= CharacterDB.GetAmount())
        {
            selected = 0;
        }

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

        characterCache[selected].SetActive(true);
    }

    public void StartButtonPress()
    {
        PlayerPrefs.SetInt("character", selected);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    
    private void InitCharacters()
    {
        foreach (GameObject prefab in CharacterDB.GetAll())
        {
            characterCache.Add(Instantiate(prefab,new Vector3(0, 0, 0),Quaternion.identity, GameObject.FindWithTag("CharacterRoot").transform));
        }
    }
}

