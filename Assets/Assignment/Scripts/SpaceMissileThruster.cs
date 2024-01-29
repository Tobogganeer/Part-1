using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMissileThruster : MonoBehaviour
{
    public SpaceMissile parent;
    public GameObject coast;
    public GameObject thruster;

    // Keep the thruster on while we are thrusting
    void Update()
    {
        coast.SetActive(parent.timer <= 0);
        thruster.SetActive(parent.timer > 0);
    }
}
