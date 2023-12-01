using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapManager : MonoBehaviour
{
    public GameObject mapViewCameraPrefab;
    public Vector3 mapViewCameraPosition;
    public Quaternion mapViewCameraRotation;
    
    private GameObject _mapViewCamera;

    private void Start()
    {
        _mapViewCamera = Instantiate(mapViewCameraPrefab, mapViewCameraPosition, mapViewCameraRotation);
    }

    private void OnViewMap(InputValue value)
    {
        GameObject playerCamera = Global.GetPlayer().GetCamera().gameObject;
        if (playerCamera.activeSelf)
        {
            playerCamera.SetActive(false);
            _mapViewCamera.SetActive(true);
            Global.GetPlayer().HideHud();
        }
        else
        {
            playerCamera.SetActive(true);
            _mapViewCamera.SetActive(false);
            Global.GetPlayer().ShowHud();
        }
    }
}
