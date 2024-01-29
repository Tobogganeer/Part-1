using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BlackHoleObject : MonoBehaviour
{
    // A list of all BlackHoleObjects
    public static List<BlackHoleObject> All { get; private set; } = new List<BlackHoleObject>();
    // Called when this thingy enters the black hole
    public Action<GameObject> OnEnterBlackHole { get; private set; }

    // Not assigned, but needs to be accessed
    [HideInInspector]
    public Rigidbody2D rb { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        All.Add(this);
    }

    private void OnDestroy()
    {
        All.Remove(this);
    }
}

/*

BlackHoleObject.cs:
- Pulled towards the black hole
- Calls a function when entering the black hole
- Pseudocode
  - Variable for rb, callback
  - Static list of BlackHoleObjects
  - Add self to list on start
  - Remove self from list on destroy
  - Action<> OnEnterBlackHole called by BlackHole.cs

*/