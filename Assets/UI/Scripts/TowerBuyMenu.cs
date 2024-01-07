using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerBuyMenu : MonoBehaviour
{
    public TextMeshProUGUI bulletUpgradeText;
    public TextMeshProUGUI laserUpgradeText;
    public Button bulletUpgradeButton;
    public Button laserUpgradeButton;

    private void Start()
    {
        TowerBuildingManager manager = TowerBuildingManager.instance;
        bulletUpgradeText.SetText("Upgrade: $" + manager.GetTowerUpgradeCost("bullet"));
        laserUpgradeText.SetText("Upgrade: $" + manager.GetTowerUpgradeCost("laser"));
    }

    public void ClickBuy(String tower)
    {
        TowerBuildingManager.instance.SetCurrentlySelectedTower(tower);
        // TODO: add hologram display stuff so you can see where you place
        Global.GetPlayer().GetHud().SendMessage("Tower selected, right click on the ground to place it", new Color32(0, 255, 0, 255), 3);
        Global.GetPlayer().GetHud().CloseTowerBuyMenu();
    }

    public void ClickUpgrade(String tower)
    {
        TowerBuildingManager manager = TowerBuildingManager.instance;
        float cost = manager.GetTowerUpgradeCost(tower);
        if (!Global.GetPlayer().HasMoney(cost)) return;
        
        Global.GetPlayer().RemoveMoney(cost);
        manager.Upgrade(tower);
        switch (tower)
        {
            case "bullet":
                if (!manager.HasAnotherUpgrade(tower))
                {
                    bulletUpgradeText.SetText("N/A");
                    bulletUpgradeButton.interactable = false;
                }
                else
                {
                    bulletUpgradeText.SetText("Upgrade: $" + manager.GetTowerUpgradeCost(tower));
                }
                break;
            case "laser":
                if (!manager.HasAnotherUpgrade(tower))
                {
                    laserUpgradeText.SetText("N/A");
                    laserUpgradeButton.interactable = false;
                }
                else
                {
                    laserUpgradeText.SetText("Upgrade: $" + manager.GetTowerUpgradeCost(tower));
                }
                break;
        }
    }

    public void ClickExit()
    {
        Global.GetPlayer().GetHud().CloseTowerBuyMenu();
    }
}
