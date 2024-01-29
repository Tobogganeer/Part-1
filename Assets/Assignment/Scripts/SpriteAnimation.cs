using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    public LoopBehaviour loopBehaviour = LoopBehaviour.Loop;
    public bool playOnStart = true;
    public float fps = 10f;
    public List<Frame> frames;

    SpriteRenderer sr;

    float timer;
    int frameIndex;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    [System.Serializable]
    public class Frame
    {
        public Sprite sprite;
        public int duration = 1; // Held for this many frames
        public float size = 1f;
    }

    public enum LoopBehaviour
    {
        Stop,
        Loop,
        Destroy
    }
}
