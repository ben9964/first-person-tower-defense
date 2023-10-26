using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public float speed;

    public float GetSpeed()
    {
        return this.speed;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
