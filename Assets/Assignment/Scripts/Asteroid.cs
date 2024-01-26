using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float angularVelocityRange;

    Rigidbody2D rb;
    float angularVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        angularVelocity = Random.Range(-angularVelocityRange, angularVelocityRange);
    }

    void Update()
    {
        // Kinematic motion - rotate over time
        transform.Rotate(0, 0, angularVelocity * Time.deltaTime);
    }
}

/*

Asteroid.cs:
- Physics objects (rigidbody) with a collider
- Instantiated outside of screen, flying towards the center (with a velocity)
- Have randomized size
- Spins slightly over time
- Will be affected by black hole (won't explode)
- Destroyed by player missiles (perhaps with a little effect)
- Destroyed after leaving screen (handled by border)
- Pseudocode
  - Variables for the rb, angular velocity
  - Set velocity towards the screen in Start
  - Rotates in Update
  - Exploding due to missiles is handled by missiles
  - Function Explode() (shoots rock particles out maybe)

*/
