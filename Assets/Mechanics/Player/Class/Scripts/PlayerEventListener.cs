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

    private void OnEscape(InputValue value)
    {
        AbstractPlayer player = transform.parent.GetComponent<AbstractPlayer>();
        HudManager hud = player.GetHud();
        if (hud.IsPaused())
        {
            hud.CloseEscMenu();
        }
        else
        {
            hud.OpenEscMenu();
        }
        
    }

    private void OnWaveStart(InputValue value)
    {
        WaveSpawner spawner = Global.GetWaveSpawner();
        if (spawner.CanStartNext())
        {
            spawner.NextWave();
            transform.parent.GetComponent<AbstractPlayer>().GetHud().HideWaveSpawnText();
        }
    }
}
