public class ExtendPowerup : Powerup
{
    public override void Consume(Paddle paddle)
    {
        paddle.GetComponent<ExtendAbility>().Activate();
    }
}
