using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public float speed = 50f;
    public float maxSpeed = 100f; // Very high
    public float rotationSpeed = 300f;
    public float maxRotationSpeed = 360f;
    public float fireRateRPM = 30f;

    [Space]
    public GameObject missilePrefab;

    Rigidbody2D rb;
    new Collider2D collider;
    float fireTimer;
    Vector2 input;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        input = new Vector2(-Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        fireTimer -= Time.deltaTime;

        // Fire a missile if we are holding space and have waited long enough
        if (Input.GetKey(KeyCode.Space) && fireTimer <= 0)
        {
            ShootMissile();
        }
    }

    void FixedUpdate()
    {
        if (input.y != 0)
            rb.AddForce(transform.up * input.y * speed * Time.deltaTime);

        float curSpeedSqr = rb.velocity.sqrMagnitude;
        float maxSpeedSqr = maxSpeed * maxSpeed;
        // Don't let us get going too fast
        if (curSpeedSqr > maxSpeedSqr)
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

        float curRotation = rb.angularVelocity;
        if (input.x > 0 && curRotation < maxRotationSpeed)
            rb.AddTorque(input.x * rotationSpeed * Time.deltaTime);
        if (input.x < 0 && curRotation > -maxRotationSpeed)
            rb.AddTorque(input.x * rotationSpeed * Time.deltaTime);
    }

    void ShootMissile()
    {
        // Gotta wait a hot second
        fireTimer = 60f / fireRateRPM;
        GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
        // Make the missile not collide with us
        // Do it this way so it will still collide with other players (if ever added)
        Physics2D.IgnoreCollision(collider, missile.GetComponent<Collider2D>());
    }
}

/*

Spaceship.cs (player):
- Accelerates with W/S, rotates with A/D
- Shoots missiles with space
- Physics based (has a Rigidbody2D), and will coast around (like space)
- Has a collider for detecting collisions
- Destroyed on impact with an asteroid
- Will have a BlackHoleObject script once it is created (to get pulled towards black hole)
- Destroyed on being sucked into black hole
- Pseudocode:
  - Variables for the rb, speed, maxSpeed, rotation speed, fire rate, fire timer, input
  - Store input and check for missile launches in Update
  - Instantiate a missile if the timer is low enough and space is pressed
  - Apply forces and torque in FixedUpdate
  - Detect asteroid collision in OnCollisionEnter2D (check tag) and destroy if colliding with an asteroid

*/
