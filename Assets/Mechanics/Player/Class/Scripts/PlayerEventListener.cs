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
        // if we're in the buy menu we dont want to pause
        if (hud.IsInBuyMenu()) return;
        
        if (hud.IsPaused())
        {
            hud.CloseEscMenu();
        }
        else
        {
            hud.OpenEscMenu();
        }

    }

    private void OnBuyMenu(InputValue value)
    {
        AbstractPlayer player = transform.parent.GetComponent<AbstractPlayer>();
        HudManager hud = player.GetHud();

        // if we're paused we dont want to open the buy menu on top
        if (hud.IsPaused()) return;
        
        if (hud.IsInBuyMenu())
        {
            hud.CloseTowerBuyMenu();
        }
        else
        {
            hud.OpenTowerBuyMenu();
        }
    }

    private void OnWaveStart(InputValue value)
    {
        AbstractPlayer player = transform.parent.GetComponent<AbstractPlayer>();
        HudManager hud = player.GetHud();

        // if we're paused we dont want to be able to control waves
        if (hud.IsPaused()) return;
        
        WaveSpawner spawner = Global.GetWaveSpawner();
        if (spawner.CanStartNext())
        {
            spawner.NextWave();
            transform.parent.GetComponent<AbstractPlayer>().GetHud().HideWaveSpawnText();
        }
    }

    private void OnUseAbility(InputValue value)
    {
        AbstractPlayer player = transform.parent.GetComponent<AbstractPlayer>();
        player.UseAbility();
    }

    private void OnMap(InputValue value)
    {
        GameObject obj = GameObject.FindWithTag("MapView");
        MapCamera mapCamera = obj.GetComponent<MapCamera>();
        mapCamera.OnViewMap(value);
    }
}
