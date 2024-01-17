using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    Rigidbody2D rb;
    [HideInInspector] public Vector2 input;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        Vector2 force = input * speed * Time.fixedDeltaTime; // Same as Time.deltaTime in this context, 0.02s
        rb.AddForce(force, ForceMode2D.Force);
    }
}
