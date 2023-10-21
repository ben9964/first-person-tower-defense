using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameGraphics : MonoBehaviour
{

    private static GameGraphics Instance { get; set; }

    public TextMeshProUGUI dieText;

    void Start()
    {
        Instance = this;
    }

    public static void ShowDeathMessage()
    {
        Instance.dieText.gameObject.SetActive(true);
    }

    public static void HideDeathMessage()
    {
        Instance.dieText.gameObject.SetActive(false);
    }
}
