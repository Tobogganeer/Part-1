using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used primarily to spin black hole rings
public class Spinner : MonoBehaviour
{
    public float spinSpeed = 360f;

    void Update()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }
}
