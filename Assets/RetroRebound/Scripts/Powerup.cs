using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    public PowerupType type;

    /// <summary>
    /// Consumes the powerup
    /// Override this for a specific powerup
    /// </summary>
    /// <param name="paddle"></param>
    public abstract void Consume(Paddle paddle);

    /// <summary>
    /// Checks if powerup collides with paddle
    /// If true, <see cref="Consume(Paddle)"/>
    /// then destroy the gameobject
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Paddle")
        {
            //Remove particular power up from manager list
            PowerupManager.Instance.ClearPowerup(this);
            Consume(collision.GetComponent<Paddle>());
            Destroy(gameObject);
        }
    }
}
