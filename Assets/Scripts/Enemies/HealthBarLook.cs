using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarLook : MonoBehaviour
{
    private GameObject _mainCamera;
    
    void Update()
    {
        if (_mainCamera == null)
        {
            if (Global.GetPlayer() == null) return;
            _mainCamera = Global.GetPlayer().GetCamera().gameObject;
        }
        if (_mainCamera == null) return;

        if (!_mainCamera.activeSelf)
        {
            return;
        }
        gameObject.transform.LookAt(_mainCamera.transform);
    }
}
