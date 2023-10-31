using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Util
{
    // from SimoGecko on Unity forums (ability to invoke with a delay using lambdas)
    public static void Invoke(this MonoBehaviour mb, Action f, float delay)
    {
        mb.StartCoroutine(InvokeRoutine(f, delay));
    }
 
    private static IEnumerator InvokeRoutine(System.Action f, float delay)
    {
        yield return new WaitForSeconds(delay);
        f();
    }

    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}
