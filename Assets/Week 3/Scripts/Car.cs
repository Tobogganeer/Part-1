using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float steeringSpeed = 300f;
    public float forwardSpeed = 1000f;
    public float maxSpeed = 100f;

    Vector2 input;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        float torque = steeringSpeed * -input.x * Time.deltaTime;
        // Only rotate if we are trying to move forwards or backwards
        // This also corrects our backwards steering
        rb.AddTorque(torque * input.y * rb.mass);

        if (rb.velocity.sqrMagnitude < (maxSpeed * maxSpeed))
        {
            // Do float operations first for better performance (humungous 0.02us savings)
            Vector3 force = transform.up * (input.y * forwardSpeed * Time.deltaTime);
            rb.AddForce(force * rb.mass);
        }
    }
}
