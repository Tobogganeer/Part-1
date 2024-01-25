using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSlick : MonoBehaviour
{
    public float dragMultiplier = 0.2f;
    Dictionary<Rigidbody2D, SlidingObject> objects = new Dictionary<Rigidbody2D, SlidingObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null && !objects.ContainsKey(collision.attachedRigidbody))
        {
            // Lower the drag and store it for later
            SlidingObject obj = new SlidingObject(collision.attachedRigidbody, dragMultiplier);
            objects[collision.attachedRigidbody] = obj;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null && objects.TryGetValue(collision.attachedRigidbody, out SlidingObject obj))
        {
            // Reset the rb's drag and remove it from the dictionary
            obj.Reset();
            objects.Remove(collision.attachedRigidbody);
        }
    }

    class SlidingObject
    {
        public float oldDrag;
        public Rigidbody2D rb;

        public SlidingObject(Rigidbody2D rb, float dragMultiplier)
        {
            this.oldDrag = rb.drag;
            this.rb = rb;
            rb.drag *= dragMultiplier;
        }

        public void Reset()
        {
            // Could I just divide the rb's drag by the dragMultiplier?
            // Yes. But this system works if I change the drag multiplier at runtime
            rb.drag = oldDrag;
        }
    }
}
