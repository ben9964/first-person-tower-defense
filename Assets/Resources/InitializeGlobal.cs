using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeGlobal : MonoBehaviour
{
    public GameObject playerHudCanvasPrefab;
    
    void Start()
    {
        Instantiate(playerHudCanvasPrefab, playerHudCanvasPrefab.transform.position,
            playerHudCanvasPrefab.transform.rotation);
    }
}
