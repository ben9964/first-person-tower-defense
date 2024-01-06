using System;
using UnityEngine;

public class TankAbstractPlayer : AbstractPlayer
{
    //make tank player freeze all enemies for 5 seconds
    public override void UseAbility()
    {
        if (!_canUseAbility) return;
        base.UseAbility();
        foreach (var obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Enemy enemy = obj.GetComponent<Enemy>();
            float oldSpeed = enemy.GetAgent().speed;
            enemy.GetAgent().speed = 0;
            this.Invoke(() =>
            {
                enemy.GetAgent().speed = oldSpeed;
            }, 5f);
        }
    }
}
