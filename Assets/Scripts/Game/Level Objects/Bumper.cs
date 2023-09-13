// Bumper script
// by: Halen
// created: 13/09/2023
// last edited: 13/09/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [Tooltip("Mulitplier for the speed the bumper gives the player. Default is 1.")]
    [Min(0)]
    public float speedMultiplier = 3f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Get the rigidbody component of the player
            Rigidbody player = other.gameObject.GetComponentInParent<Rigidbody>();
            // Get the player's current speed
            float speed = player.velocity.magnitude;
            // Stop the player
            player.velocity = Vector3.zero;
            // Apply a force in the direction of the bumper based on the player's inital speed
            // AND the multiplier of the bumper
            player.AddForce(transform.forward * speed * speedMultiplier, ForceMode.Impulse); // fucken send it
        }
    }

}
