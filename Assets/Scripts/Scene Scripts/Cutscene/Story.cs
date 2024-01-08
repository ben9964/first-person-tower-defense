using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    void Start()
    {
        //Invokes LoadNextScene method after short delay
        Invoke("LoadNextScene", 15.8f);
    }

    void LoadNextScene()
    {
        //loads easy scene right after cutscene plays
        SceneManager.LoadScene("Easy");
    }
}




