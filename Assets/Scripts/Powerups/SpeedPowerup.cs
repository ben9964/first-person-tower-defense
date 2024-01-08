public class SpeedPowerup : Powerup
{
    public float _speedMultiplier = 1.5f;
    public float duration = 10f;
    
    protected override void ApplyEffect(AbstractPlayer player)
    {
        float oldSpeed = player.speed;
        player.speed *= _speedMultiplier;
        this.Invoke(() =>
        {
            player.speed = oldSpeed;
        }, duration);
    }
}
