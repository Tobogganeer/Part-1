using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarParticles : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject particlePrefab;
    public float emissionMultiplier = 5f; // Multiplied with velocity
    public float particleAngularVelocity = 180f;

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
            particles.Add(new Particle(Random.Range(-particleAngularVelocity, particleAngularVelocity), particle));
        }

        for (int i = particles.Count - 1; i >= 0; i--)
        {
            Particle particle = particles[i];
            if (particle == null)
            {
                particle.obj.transform.Rotate()
            }
            else
            {
                particles.RemoveAt(i);
            }
        }
    }

    class Particle
    {
        public float angularVelocity;
        public GameObject obj;

        public Particle(float angularVelocity, GameObject obj)
        {
            this.angularVelocity = angularVelocity;
            this.obj = obj;
        }
    }
}
