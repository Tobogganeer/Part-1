using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone : MonoBehaviour
{
    public GameObject standingObject;
    public GameObject knockedOverObject;

    public float minimumImpactVelocity = 10f;
    bool knocked;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (knocked)
            return;

        if (collision.relativeVelocity.sqrMagnitude > (minimumImpactVelocity * minimumImpactVelocity))
        {
            knocked = true;
            standingObject.SetActive(false);
            knockedOverObject.SetActive(true);

            // Face in direction we are being hit
            Vector3 direction = collision.relativeVelocity.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
