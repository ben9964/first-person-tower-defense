public class DamagePowerup : Powerup
{
    public float extraDamage = 50f;
    public float duration = 10f;
    
    protected override void ApplyEffect(AbstractPlayer player)
    {
        float oldDamage = player.GetCurrentWeapon().GetDamage();
        player.GetCurrentWeapon().SetDamage(oldDamage + extraDamage);
        this.Invoke(() =>
        {
            player.GetCurrentWeapon().SetDamage(oldDamage);
        }, duration);
    }
}
