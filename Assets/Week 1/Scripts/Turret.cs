using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float rotationSpeed = 180f;

    void Update()
    {
        float rotationInput = Input.GetAxis("Vertical");
        transform.Rotate(Vector3.forward, rotationInput * rotationSpeed * Time.deltaTime);
    }
}
