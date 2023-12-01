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
            _mainCamera = GameObject.FindWithTag("MainCamera");
        }

        if (!_mainCamera.activeSelf)
        {
            return;
        }
        gameObject.transform.LookAt(_mainCamera.transform);
    }
}
