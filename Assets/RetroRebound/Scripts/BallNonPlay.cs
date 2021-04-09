using UnityEngine;

public class BallNonPlay : MonoBehaviour
{
    Rigidbody2D rb; // Rigidbody of the ball

    [Header("Ball Movement")]
    public float moveSpeed1 = 2f;
    public float moveSpeed2 = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed1, moveSpeed2);
    }
}
