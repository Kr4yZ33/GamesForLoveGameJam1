using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Collider2D collide;
    public Animator animator;

    private bool isDestroyed;

    private void Update()
    {
        if(isDestroyed)
        {
            return;
        }
        transform.Translate(transform.up * moveSpeed);
    }

    public void Die()
    {
        isDestroyed = true;
        collide.enabled = false;

        animator.Play("Impact");
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            Die();
        }
    }
}
