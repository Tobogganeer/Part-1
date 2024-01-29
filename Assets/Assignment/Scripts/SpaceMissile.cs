using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMissile : MonoBehaviour
{
    public float accelerationForce = 250f;
    public float accelerationTime = 3f;

    Rigidbody2D rb;
    [HideInInspector]
    public float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = accelerationTime;

        // Hardcoded lifetime
        Destroy(gameObject, 20f);
    }

    void FixedUpdate()
    {
        // Accelerate for just a bit
        timer -= Time.deltaTime;

        if (timer > 0)
            rb.AddRelativeForce(Vector2.up * (accelerationForce * rb.mass * Time.deltaTime));

        // It works better without rotation at all
        /*
        // Don't change rotation if we are going too slow
        if (rb.velocity.sqrMagnitude > 1)
        {
            // Eyes on the road
            Vector2 direction = rb.velocity.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // Correct for Unity rotation being 90 degrees behind trig rotation
            rb.MoveRotation(angle - 90);
        }
        */
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if this is an asteroid
        if (collision.gameObject.CompareTag(SpaceBattleManager.AsteroidTag))
        {
            // Kaboom
            collision.gameObject.GetComponent<Asteroid>().BlowUp();
            SpaceBattleManager.Explode(gameObject);
        }
    }
}

/*

SpaceMissile.cs (missile):
- Physics object with collider
- Accelerates over a short time and then cuts the "thruster" and coasts
- Affected by black hole & explodes upon entering
- Faces towards its velocity vector
- Destroys asteroids
- Pseudocode
  - Variables for rb, accelerationForce, accelerationTime, timer
  - Set black hole callback in Start to explode
  - Decrement & check timer and apply forces in FixedUpdate
  - Rotate towards velocity in Update using Atan2
  - Check for collision with asteroid in OnTriggerEnter2D and blow it up
  - Function Explode() (explosion particles/sprites/animation)

*/
