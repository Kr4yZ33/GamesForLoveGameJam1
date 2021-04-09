public class AmmoPowerup : Powerup
{
    public override void Consume(Paddle paddle)
    {
        paddle.GetComponent<ShootGunAbility>().Activate();
    }
}
