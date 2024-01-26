using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    /// <summary>
    /// Returns a random point inside the area that the player can see.
    /// </summary>
    public static Vector2 GetRandomPointInWorld()
    {
        float x = Random.Range(-WorldSize.x, WorldSize.x);
        float y = Random.Range(-WorldSize.y, WorldSize.y);
        return new Vector2(x, y);
    }


    /// <summary>
    /// Returns true if <paramref name="point"/> is in the area the player can see.
    /// </summary>
    public static bool IsPointInWorld(Vector2 point)
    {
        Rect rect = new Rect(Vector2.zero, WorldSize);
        return rect.Contains(point);
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
