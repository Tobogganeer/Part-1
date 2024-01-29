using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public int numStars = 200;

    public GameObject[] starPrefabs;

    private void Start()
    {
        // Just fill the sky with stars
        for (int i = 0; i < numStars; i++)
        {
            GameObject prefab = starPrefabs[Random.Range(0, starPrefabs.Length)];
            Vector2 position = SpaceBattleManager.GetRandomPointInWorld();
            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));
            Instantiate(prefab, position, rotation, transform);
        }
    }
}
