using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 10f;
    
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
