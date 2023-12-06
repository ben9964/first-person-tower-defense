using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyByCamera : MonoBehaviour
{

    void Start()
    {
        // Call EndCutscene after 9.2 seconds
        Invoke("EndCutscene", 9.2f);
    }

    void EndCutscene()
    {
        // Destroy this camera
        Destroy(this.gameObject);
    }
}