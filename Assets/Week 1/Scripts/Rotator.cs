using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 360f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, -speed * Time.deltaTime);
    }
}
