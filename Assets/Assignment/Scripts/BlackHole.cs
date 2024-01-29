using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float mass = 10000f;
    public float gravityMultiplier = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ApplyULOG(Rigidbody2D other)
    {
        // Fg = (G * m1 * m2) / d^2
        // G = 6.673x10^-11
        // Float is too imprecise for this
        const double G = 0.00000000006673d;

        Vector2 displacement = other.position - (Vector2)transform.position;
        float distanceSqr = displacement.sqrMagnitude;
        double force = G * gravityMultiplier * mass * other.mass;
        force /= distanceSqr;
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