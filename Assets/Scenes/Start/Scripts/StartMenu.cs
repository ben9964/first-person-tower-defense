using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public String nextScene;

    public void LoadScene()
    {
        Util.LoadScene(nextScene);
    }
}
