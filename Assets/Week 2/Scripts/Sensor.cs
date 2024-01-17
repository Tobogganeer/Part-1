using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color enabledColour = Color.red;
    public Color disabledColour = Color.green;

    void Start()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = disabledColour;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"{collision.gameObject} is in the trigger");
        if (spriteRenderer != null)
            spriteRenderer.color = enabledColour;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (spriteRenderer != null)
            spriteRenderer.color = disabledColour;
    }
}
