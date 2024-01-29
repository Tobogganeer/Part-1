using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipThrusters : MonoBehaviour
{
    public GameObject forwardThrust;
    public GameObject backwardsThrust;

    void Update()
    {
        forwardThrust.SetActive(Input.GetKey(KeyCode.W));
        backwardsThrust.SetActive(Input.GetKey(KeyCode.S));
    }
}
