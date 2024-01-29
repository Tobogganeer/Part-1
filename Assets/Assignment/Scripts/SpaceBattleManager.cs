using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpaceBattleManager : MonoBehaviour
{
    public static SpaceBattleManager Instance { get; private set; }

    public List<Sprite> asteroidSprites;
    public GameObject explosionPrefab;
    public GameObject asteroidPrefab;
    public int numAsteroids = 10;

    [Space]
    public GameObject playerPrefab;
    public float playerSpawnRadius = 10f;
    public GameObject deathScreen;
    public float deathTime = 3f;
    public SpriteRenderer deathCountdown;
    public List<Sprite> numberSprites;

    public static Vector2 WorldSize { get; private set; }
    public static Rect WorldRect { get; private set; }
    public static readonly string SpaceshipTag = "Spaceship";
    public static readonly string AsteroidTag = "Asteroid";
    public static readonly string SpaceMissileTag = "SpaceMissile";

    bool quitting;
    float deathTimer;

    private void Awake()
    {
        Instance = this;
        CalculateWorldSize();
    }

    private void Start()
    {
        // Add some asteroids
        for (int i = 0; i < numAsteroids; i++)
        {
            SpawnAsteroid();
        }

        // Spawn the player
        SpawnPlayer();
        // Don't show the death screen
        deathScreen.SetActive(false);
    }

    void CalculateWorldSize()
    {
        Camera cam = Camera.main;
        // The orthographic size is the vertical extents
        WorldSize = new Vector2(cam.orthographicSize * 2f * cam.aspect, cam.orthographicSize * 2f);
        WorldRect = new Rect(-WorldSize / 2, WorldSize);
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
        return WorldRect.Contains(point);
    }

    public static void SpawnExplosion(Vector3 position)
    {
        Instantiate(Instance.explosionPrefab, position, Quaternion.identity);
    }

    public static void Explode(GameObject obj)
    {
        SpawnExplosion(obj.transform.position);
        Destroy(obj);
    }

    public static void SpawnAsteroid()
    {
        // Spawn them in a random place (don't hit the black hole)
        if (!Instance.quitting)
            Instantiate(Instance.asteroidPrefab, new Vector3(0, 100, 0), Quaternion.identity);
    }

    void SpawnPlayer()
    {
        // Random spawn position around the black hole
        Vector2 spawnPosition = Random.insideUnitCircle.normalized * playerSpawnRadius;
        // Random rotation
        Quaternion spawnRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

        Instantiate(playerPrefab, spawnPosition, spawnRotation);
    }

    public static void OnPlayerDied()
    {
        // I'm not gonna support numbers over 9
        Instance.deathTimer = Mathf.Min(Instance.deathTime, 9f);
        Instance.deathScreen.SetActive(true);
    }

    private void Update()
    {
        if (deathTimer > 0)
        {
            deathTimer -= Time.deltaTime;

            if (deathTimer <= 0)
            {
                deathScreen.SetActive(false);
                SpawnPlayer();
            }
            else
            {
                deathCountdown.sprite = numberSprites[Mathf.CeilToInt(deathTimer)];
            }
        }
    }



    private void OnApplicationQuit()
    {
        quitting = true;
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

        Gizmos.color = Color.red;

        // Show where the player will spawn
        Gizmos.DrawWireSphere(transform.position, playerSpawnRadius);
    }
}
