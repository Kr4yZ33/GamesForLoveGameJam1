// Created by team AdaptEvolve for the 
// GamesForLove Game Jam 1 April 2020
// Primary contributers: Jeremy Pernesz

using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb; // Rigidbody of the ball

    [Header("References")]
    public bool nonGameBall;
    public float baseMoveSpeed = 3f;
    public float moveSpeedGrowth = 0.5f;
    private float _currentMoveSpeed;

    [Header("SFX")]
    public AudioSource audioSource; // 2D audiosource used for SFX
    public AudioClip wallBounceSound; // Audio clip for bounce SFX
    public float volume = 0.5f; // Volume of the audioclip
    public ParticleSystem bounceParticle; // reference to the particle effect we will use
    // When the prefab is spawned assign audio source
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // On Start get the rigidbody of the ball
    // then apply a starting velocity and direction to the ball
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _currentMoveSpeed = baseMoveSpeed + GameManager.Instance.LevelIteration * moveSpeedGrowth;
    }

    void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * _currentMoveSpeed;
    }

    // On collision with something in the layer wall
    // call the BallCollisionHandler method

    void OnCollisionEnter2D(Collision2D other)
    {
        
        // if the object is not tagged Wall or Block, exit
        if (!other.gameObject.CompareTag("Wall") | !other.gameObject.CompareTag("Block"))
            return;

        else
        {
            BallCollisionHandler();
        }
    }

    // Play the bounce effects
    void BallCollisionHandler()
    {
        //Debug.Log("Play bounce clip");
        audioSource.PlayOneShot(wallBounceSound, volume);
        bounceParticle.Play(); // start playing the particle effect (modify effect duration within the particle effect on the ball prefab
    }

    public void ChangeVelocity(Vector2 newVelocity)
    {
        GetComponent<Rigidbody2D>().velocity = newVelocity;
    }

    public Vector2 Velocity()
    {
        return rb.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BottomCollider")
        {
            //Remove Ball from manager list becaue it is no longer in play
            BallsManager.Instance.BallsInPlay.Remove(this);
            Destroy(this.gameObject);
            
        }
    }
}
