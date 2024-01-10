using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float keyboardInput = Input.GetAxis("Horizontal");
        transform.Translate(keyboardInput * speed * Time.deltaTime, 0, 0);
    }
}
