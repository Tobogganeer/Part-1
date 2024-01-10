using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float rotationSpeed = 180f;
    public float minimumAngle = -5f;
    public float maximumAngle = 70f;

    void Update()
    {
        float rotationInput = Input.GetAxis("Vertical");
        float barrelAngle = transform.eulerAngles.z;
        if ((barrelAngle > minimumAngle && rotationInput < 0) || (barrelAngle < maximumAngle && rotationInput > 0))
        {
            transform.Rotate(Vector3.forward, rotationInput * rotationSpeed * Time.deltaTime);
        }
    }
}
