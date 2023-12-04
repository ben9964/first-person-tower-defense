using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerBuyMenu : MonoBehaviour
{
    public void ClickBuy(String tower)
    {
        TowerBuildingManager.instance.SetCurrentlySelectedTower(tower);
        // TODO: add hologram display stuff so you can see where you place
        Global.GetPlayer().GetHud().SendMessage("Tower selected, right click on the ground to place it", new Color32(0, 255, 0, 255));
        Global.GetPlayer().GetHud().CloseTowerBuyMenu();
    }

    public void ClickUpgrade(String tower)
    {
        Global.GetPlayer().GetHud().SendMessage("COMING SOON", new Color32(255, 255, 0, 255));
    }

    public void ClickExit()
    {
        Global.GetPlayer().GetHud().CloseTowerBuyMenu();
    }
}
