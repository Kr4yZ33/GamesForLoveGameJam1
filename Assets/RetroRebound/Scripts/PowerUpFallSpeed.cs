using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFallSpeed : MonoBehaviour
{
    Rigidbody2D rb;
    readonly float maxSpeed = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = rb.velocity.normalized * maxSpeed;
    }
}
