using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float cameraSpeed;
    private void Update()
    {
        transform.Rotate(0, cameraSpeed * Time.deltaTime, 0);
    }
}
