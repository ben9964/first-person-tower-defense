using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapCamera : MonoBehaviour
{
    public void OnViewMap(InputValue value)
    {
        GameObject playerCamera = Global.GetPlayer().GetCamera().gameObject;
        if (playerCamera.activeSelf)
        {
            Global.SetInMapView(true);
            playerCamera.SetActive(false);
            gameObject.GetComponent<Camera>().enabled = true;
            gameObject.GetComponent<AudioListener>().enabled = true;
            Global.GetPlayer().HideHud();
            Global.GetPlayer().GetCurrentWeapon().ResetShot();
        }
        else
        {
            playerCamera.SetActive(true);
            gameObject.GetComponent<Camera>().enabled = false;
            gameObject.GetComponent<AudioListener>().enabled = false;
            Global.GetPlayer().ShowHud();
            Global.SetInMapView(false);
        }
    }
}
