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

    bool playing;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        // Turn ourselves off if there is no animation to play
        if (frames.Count == 0)
            enabled = false;
        // Otherwise get us ready to display the first frame
        else
            UpdateCurrentFrame();

        playing = playOnStart;
    }

    void Update()
    {
        if (!playing)
            return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            frameIndex++;

            if (frameIndex >= frames.Count)
            {
                // We have played everything
                OnFinalFrameDisplayed();
            }
            else
            {
                // Keep 'er going
                UpdateCurrentFrame();
            }
        }
    }

    void OnFinalFrameDisplayed()
    {
        switch (loopBehaviour)
        {
            case LoopBehaviour.Stop:
                playing = false;
                break;
            case LoopBehaviour.Loop:
                frameIndex = 0;
                UpdateCurrentFrame();
                break;
            case LoopBehaviour.Destroy:
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    public void Play(bool reset = false)
    {
        playing = true;
        // If we want to reset OR we have finished and stopped playing, reset to the start
        if (reset || loopBehaviour == LoopBehaviour.Stop && frameIndex >= frames.Count)
            frameIndex = 0;

        UpdateCurrentFrame();
    }

    public void Pause()
    {
        playing = false;
    }

    public void Stop()
    {
        playing = false;
        ResetFrame();
    }

    public void ResetFrame()
    {
        frameIndex = 0;
        UpdateCurrentFrame();
    }

    void UpdateCurrentFrame()
    {
        Frame f = frames[frameIndex];
        timer = f.duration * 1f / fps;
        sr.sprite = f.sprite;
        transform.localScale = Vector3.one * f.size;
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
