using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // This bit of code runs before any scenes load and allows us to initialize global utilities such as the character
    // database without needing to put an object in every scene OR go through the character select every time we want to
    // test a given scene.
    //
    // found on LowScope's blog for global script initialization without using a dedicated scene
    // https://low-scope.com/unity-tips-1-dont-use-your-first-scene-for-global-script-initialization/
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadMain()
    {
        GameObject main = GameObject.Instantiate(Resources.Load("Main")) as GameObject;
        GameObject.DontDestroyOnLoad(main);
    }
}
