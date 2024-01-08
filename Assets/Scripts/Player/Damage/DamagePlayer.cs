using System;


public class DamageAbstractPlayer : AbstractPlayer
{
    //Damage character gets a no shoot cooldown ability for 5 seconds
    public override void UseAbility()
    {
        if (!_canUseAbility) return;
        base.UseAbility();
        float oldShootCooldown = GetCurrentWeapon().shootCooldown;
        GetCurrentWeapon().shootCooldown = GetCurrentWeapon().shootCooldown / 2;
        this.Invoke(() =>
        {
            GetCurrentWeapon().shootCooldown = oldShootCooldown;
        }, 5f);
    }
}
