using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public Transform playerTeleportPosition;
    public Axis playerTeleportAxis; // We either teleport them up/down or left/right, need to know which

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(SpaceBattleManager.AsteroidTag) || collision.CompareTag(SpaceBattleManager.SpaceMissileTag))
        {
            // Delete these yucky things
            SpaceBattleManager.Explode(collision.gameObject);
        }
        else if (collision.CompareTag(SpaceBattleManager.SpaceshipTag))
        {
            // Teleport them to the other side of the screen
            if (playerTeleportAxis == Axis.Vertical)
            {
                collision.transform.position = collision.transform.position.With(y: playerTeleportPosition.position.y);
            }
            else
            {
                collision.transform.position = collision.transform.position.With(x: playerTeleportPosition.position.x);
            }
        }
    }

    public enum Axis
    {
        Horizontal,
        Vertical
    }
}

// I don't want to do weird shenanigans to set one value of a transform's position
public static class Vec3Extensions
{
    public static Vector3 With(this Vector3 v, float? x = null, float? y = null, float? z = null)
    {
        // Sub in the values if they exist
        return new Vector3(x ?? v.x, y ?? v.y, z ?? v.z);
    }
}

/*

Border.cs:
- Checks for players entering the trigger and wraps them around the screen
- Checks for asteroids/missiles entering and deletes them
- One on each side of the screen
- Pseudocode
  - Variables for tags (for player, asteroid, missile), player teleport position
  - Check for these tags in OnTriggerEnter2D and take an effect
    - Delete missiles & asteroids
    - Teleport players to the other position specified

*/
