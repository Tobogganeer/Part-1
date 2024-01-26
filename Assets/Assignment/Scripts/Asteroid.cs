using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float angularVelocityRange = 360f;
    public Vector2 randomScale = new Vector2(0.5f, 2.5f);

    Rigidbody2D rb;
    float angularVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Make us spin
        angularVelocity = Random.Range(-angularVelocityRange, angularVelocityRange);
        // Set a randomized size
        transform.localScale = Vector3.one * Random.Range(randomScale.x, randomScale.y);

        // Set sprite to a random asteroid (all have similar size & shape, no need to change collider)
        int numSprites = SpaceBattleManager.Instance.asteroidSprites.Count;
        Sprite randomSprite = SpaceBattleManager.Instance.asteroidSprites[Random.Range(0, numSprites)];
        GetComponent<SpriteRenderer>().sprite = randomSprite;
    }

    void Update()
    {
        // Kinematic motion - rotate over time
        transform.Rotate(0, 0, angularVelocity * Time.deltaTime);
    }

    public void Explode()
    {
        // TODO: Particle effect
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
