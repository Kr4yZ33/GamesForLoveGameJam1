// Created by team AdaptEvolve for the 
// GamesForLove Game Jam 1 April 2020
// Primary contributers: Jeremy Pernesz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public enum PaddleStates { Idle, Active, Shooting, Extended }
    PaddleStates currentPaddleState;

    [Header("Paddle States")]
    // handle idle state
    public bool levelStarted;

    // handle extended state
    public bool canExtend;
    public float extendTime = 10f;

    [Header("Dependencies")]
    public Animator shieldAnimator;
    public GameObject shield;
    public bool shieldActive = true;
    public Animator paddleAnimator;

    [Header("SFX")]
    public AudioClip sfxShotClip;
    public AudioSource audioSource;
    public float volume = 0.5f;
    public ParticleSystem powerupCollectedEffect;

    [Header("Movement")]
    public float speed = 5f; // speed the paddle moves

    bool moveL; // bool for left movement, this will make it easy to hook Action Based Input into the project later
    bool moveR; // bool for right movement
    Rigidbody2D rb; // Rigidbody of the paddle

    public Transform ballSpawnPoint;

    // On start get the rigidbody of the paddle
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
    }

    // each frame call the Direction and Paddle movement methods
    private void Update()
    {
        DirectionMovement();
        PaddleMovement();
    }

    void LateUpdate()
    {
        HandleIdleState();
        HandleActiveState();
    }

    // runs every 60 frames
    void FixedUpdate()
    {
        if(shieldActive)
        {
            shield.SetActive(true);
        }
        else
        {
            shield.SetActive(false);
        }
    }

    // Used to set the bools for right and left paddle movement
    void DirectionMovement()
    {
        // get the keyboard input fo rthe left and right arrow keys
        float direction = Input.GetAxisRaw("Horizontal"); // left = -1 right = 1
        //Debug.Log(direction + " arrow direction value");
        if (direction == 1)
            moveR = true;
        else
            moveR = false;

        if (direction == -1)
            moveL = true;
        else
            moveL = false;
    }

    // used to add velocity to the paddle in left or right direction
    void PaddleMovement()
    {
        if (moveR)
            rb.velocity = Vector2.right * speed;

        if (moveL)
            rb.velocity = Vector2.left * speed;

        if (!moveL && !moveR) // if neither left or right direction pressed, remove all velocity
            rb.velocity = Vector2.zero;
    }

    public void StartLevel()
    {
        levelStarted = true;
    }

    public void EndLevel()
    {
        levelStarted = false;
    }

    void StartMultiball()
    {
        Debug.Log("Multiball Started");
        powerupCollectedEffect.Play();
        // spawn extra two balls where the ball currently is
    }

    void HandleIdleState()
    {
        if (levelStarted)
            return;
        else if (currentPaddleState != PaddleStates.Idle)
        {
            currentPaddleState = PaddleStates.Idle;
        }
    }

    void HandleActiveState()
    {
        if (!levelStarted)
            if (canExtend)
                return;
        else if (currentPaddleState != PaddleStates.Active)
        {
            currentPaddleState = PaddleStates.Active;
        }
    }
}
