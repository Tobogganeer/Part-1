using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float mass = 10000f;
    public float g = 6.673f; // We aren't physically accurate here

    private void FixedUpdate()
    {
        foreach (BlackHoleObject obj in BlackHoleObject.All)
        {
            ApplyULOG(obj.rb);
        }
    }

    void ApplyULOG(Rigidbody2D other)
    {
        // Fg = (G * m1 * m2) / d^2
        // G = 6.673x10^-11

        // From them to us
        Vector2 displacement = (Vector2)transform.position - other.position;
        float distanceSqr = displacement.sqrMagnitude;
        float force = g * mass * other.mass;
        force /= distanceSqr;
        // Don't apply to ourselves, we are stationary
        other.AddForce(displacement.normalized * (float)force * Time.deltaTime);
    }
}

/*

BlackHole.cs:
- Pulls BlackHoleObjects towards it using Newton's ULOG
- Spins slowly
- Destroys objects that are unfortunate enough to fall in
- Pseudocode
  - Variable for gravitational force multiplier, mass, spinRate
  - apply physics in FixedUpdate to all BlackHoleObjects
  - Detect objects entering in OnTriggerEnter2D, check for BlackHoleObject script and call callback, destroy object

*/