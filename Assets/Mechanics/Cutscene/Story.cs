using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    void Start()
    {
        Invoke("LoadNextScene", 15.8f);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("Easy");
    }
}




