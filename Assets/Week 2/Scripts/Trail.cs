using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public Player player;
    public float offset;
    TrailRenderer trailRenderer;
    float initialTime;

    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        initialTime = trailRenderer.time;
    }

    void Update()
    {
        float mag = player.input.magnitude; // Will be greater than 1 for diagonal movement
        float fac = Mathf.InverseLerp(1f, 1.414f, mag);
        float length = Mathf.Lerp(1f, 1.5f, fac);

        trailRenderer.time = initialTime * length;
    }
}
