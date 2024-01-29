using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float angularVelocityRange = 360f;
    public Vector2 randomScale = new Vector2(0.5f, 2.5f);
    public Vector2 randomVelocity = new Vector2(2f, 10f);

    Rigidbody2D rb;
    float angularVelocity;
    [HideInInspector]
    public bool hasEnteredWorld;

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

        FindRandomSpawnPoint();
    }

    void FindRandomSpawnPoint()
    {
        // This algorithm is naive and no good, but heres the idea
        // - Choose a random direction vector
        // - Move us in that direction until we leave the screen
        // - Set our velocity towards a random point in the screen

        const float StepSize = 3f; // 3 meters seems fine
        float randomAngle = Random.Range(-Mathf.PI, Mathf.PI); // mmm I love radians
        Vector2 direction = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        Vector2 step = direction * StepSize;

        Vector2 spawnPoint = Vector2.zero;
        while (SpaceBattleManager.IsPointInWorld(spawnPoint))
        {
            // I hope this loop never breaks lol
            // Edit: it broke
            spawnPoint += step;
        }

        Vector2 randomPoint = SpaceBattleManager.GetRandomPointInWorld();
        // The direction from our spawn point to the random point, normalized and given a random velocity
        rb.velocity = (randomPoint - spawnPoint).normalized * Random.Range(randomVelocity.x, randomVelocity.y);
        rb.position = spawnPoint;
    }

    void Update()
    {
        // Kinematic motion - rotate over time
        transform.Rotate(0, 0, angularVelocity * Time.deltaTime);
        
        // Check if we have entered the screen at all (to avoid being destroyed by borders)
        if (!hasEnteredWorld)
            hasEnteredWorld = SpaceBattleManager.IsPointInWorld(transform.position);
    }

    public void BlowUp()
    {
        // TODO: Particle effect
        Destroy(gameObject);
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
