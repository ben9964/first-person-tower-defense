public class HealthPowerup : Powerup
{
    public float healthRegained = 50f;
    
    protected override void ApplyEffect(AbstractPlayer player)
    {
        player.Heal(healthRegained);
    }
}
