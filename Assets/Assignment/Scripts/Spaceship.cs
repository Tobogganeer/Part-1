using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
  - Set callback for being sucked into black hole (destroy ourselves)
  - Store input and check for missile launches in Update
  - Instantiate a missile if the timer is low enough and space is pressed
  - Apply forces and torque in FixedUpdate
  - Detect asteroid collision in OnCollisionEnter2D (check tag) and destroy if colliding with an asteroid

*/
