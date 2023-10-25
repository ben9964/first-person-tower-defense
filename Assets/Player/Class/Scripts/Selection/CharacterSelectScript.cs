using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectScript : MonoBehaviour
{
    public int selected;
    public String sceneToLoad;

    private List<GameObject> characterCache = new List<GameObject>();

    public void Start()
    {
        InitCharacters();
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
        CharacterDB.SetSelected(this.selected);
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
    
    private void InitCharacters()
    {
        foreach (GameObject prefab in CharacterDB.GetAll())
        {
            characterCache.Add(Instantiate(prefab.transform.Find("PlayerCapsule/Model").gameObject,new Vector3(0, 0, 0), Quaternion.identity, GameObject.FindWithTag("CharacterRoot").transform));
        }
    }
}

