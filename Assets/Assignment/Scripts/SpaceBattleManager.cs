using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBattleManager : MonoBehaviour
{
    public static SpaceBattleManager Instance { get; private set; }

    public List<Sprite> asteroidSprites;

    private void Awake()
    {
        Instance = this;
    }
}
