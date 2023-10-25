using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionListener : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        transform.parent.GetComponent<AbstractPlayer>().HandleCollision(other);
    }

    private void OnLeftClick(InputValue value)
    {
        transform.parent.GetComponent<AbstractPlayer>().HandleUseWeapon(value);
    }
}
