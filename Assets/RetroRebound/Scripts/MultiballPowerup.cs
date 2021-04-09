public class MultiballPowerup : Powerup
{
    public override void Consume(Paddle paddle)
    {
        BallsManager.Instance.DuplicateBalls();
    }
}
