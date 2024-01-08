using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapCamera : MonoBehaviour
{
    private GameObject ceiling;

    private void Start()
    {
        gameObject.GetComponent<AudioListener>().enabled = false;
    }

    public void OnViewMap(InputValue value)
    {
        GameObject playerCamera = Global.GetPlayer().GetCamera().gameObject;
        if (playerCamera.activeSelf)
        {
            Global.SetInMapView(true);
            playerCamera.SetActive(false);
            gameObject.GetComponent<Camera>().enabled = true;
            gameObject.GetComponent<AudioListener>().enabled = true;
            if (GameObject.FindWithTag("Ceiling") != null)
            {
                ceiling = GameObject.FindWithTag("Ceiling");
                ceiling.SetActive(false);
            }
            Global.GetPlayer().HideHud();
            Global.GetPlayer().GetCurrentWeapon().ResetShot();
        }
        else
        {
            playerCamera.SetActive(true);
            gameObject.GetComponent<Camera>().enabled = false;
            gameObject.GetComponent<AudioListener>().enabled = false;
            if (ceiling != null)
            {
                ceiling.SetActive(true);
            }
            Global.GetPlayer().ShowHud();
            Global.SetInMapView(false);
        }
    }
}
