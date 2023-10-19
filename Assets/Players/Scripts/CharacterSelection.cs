using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Players.Scripts
{
    public class CharacterSelection : MonoBehaviour
    {
        public int selected;

        public void Start()
        {
            Characters.Instance.GetCharacter(selected).SetActive(true);
        }

        public void SelectNext()
        {
            Characters.Instance.GetCharacter(selected).SetActive(false);
            selected += 1;
            if (selected >= Characters.Instance.getAmount())
            {
                selected = 0;
            }
            Characters.Instance.GetCharacter(selected).SetActive(true);
        }

        public void SelectPrevious()
        {
            Characters.Instance.GetCharacter(selected).SetActive(false);
            selected -= 1;
            if (selected < 0)
            {
                selected = Characters.Instance.getAmount() - 1;
            }
            Characters.Instance.GetCharacter(selected).SetActive(true);
        }

        public void StartButtonPress()
        {
            PlayerPrefs.SetInt("character", selected);
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}
