using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBattleManager : MonoBehaviour
{
    public static SpaceBattleManager Instance { get; private set; }

    public List<Sprite> asteroidSprites;

    public static Vector2 WorldSize { get; private set; }

    private void Awake()
    {
        Instance = this;
        CalculateWorldSize();
    }

    void CalculateWorldSize()
    {
        Camera cam = Camera.main;
        // The orthographic size is the vertical extents
        WorldSize = new Vector2(cam.orthographicSize * 2f * cam.aspect, cam.orthographicSize * 2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Draw a square representing the size of the playable area
        ReadOnlySpan<Vector3> points = new ReadOnlySpan<Vector3>(new Vector3[]
        {
            new Vector3(WorldSize.x / 2, WorldSize.y / 2),
            new Vector3(WorldSize.x / 2, -WorldSize.y / 2),
            new Vector3(-WorldSize.x / 2, -WorldSize.y / 2),
            new Vector3(-WorldSize.x / 2, WorldSize.y / 2)
        });
        Gizmos.DrawLineStrip(points, true);
    }
}
