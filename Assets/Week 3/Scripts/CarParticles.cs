using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarParticles : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject particlePrefab;
    public float emissionMultiplier = 5f; // Multiplied with velocity
    public float particleAngularVelocity = 180f;
    public float velocityMultiplier = 0.4f;
    public float velocityRandomness = 0.7f;
    public Vector2 particleScale = new Vector2(0.5f, 1.2f);

    float timer;
    List<Particle> particles = new List<Particle>();

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            // Add 1 to avoid division by 0
            timer = 1f / ((rb.velocity.magnitude + 1) * emissionMultiplier);
            GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360f)));
            // Destroy the particles after 2 seconds
            Destroy(particle, 2f);
            // Basically throws the particles in the opposite direction, plus backwards a bit, plus some random direction
            Vector2 velocity = -rb.velocity * velocityMultiplier - (Vector2)rb.transform.up + Random.insideUnitCircle * velocityRandomness;
            float angularVelocity = Random.Range(-particleAngularVelocity, particleAngularVelocity);
            particles.Add(new Particle(velocity, angularVelocity, particle));
            particle.transform.localScale *= Random.Range(particleScale.x, particleScale.y);
        }

        for (int i = particles.Count - 1; i >= 0; i--)
        {
            Particle particle = particles[i];
            if (particle.obj == null)
            {
                particles.RemoveAt(i);
            }
            else
            {
                particle.obj.transform.Rotate(0, 0, particle.angularVelocity * Time.deltaTime);
                particle.obj.transform.position += (Vector3)particle.velocity * Time.deltaTime;
            }
        }
    }

    class Particle
    {
        public Vector2 velocity;
        public float angularVelocity;
        public GameObject obj;

        public Particle(Vector2 velocity, float angularVelocity, GameObject obj)
        {
            this.velocity = velocity;
            this.angularVelocity = angularVelocity;
            this.obj = obj;
        }
    }
}
