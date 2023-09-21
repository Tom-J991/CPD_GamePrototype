// Death box script
// by Jackson / Halen
// Last edited 21/9/23 9:37 AM

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBox : MonoBehaviour
{
    [Tooltip("Reference to the canvas that appears upon touching a death plane.")]
    public Canvas deathCanvas;

    void Start()
    {
        deathCanvas.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // stop all audio sources
            AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach(AudioSource source in sources)
            {
                source.Stop();
            }

            // kill the player
            Destroy(other.gameObject);
            // unlock the mouse
            FindObjectOfType<GameManager>().UnlockMouse();
            // display the game over screen
            deathCanvas.gameObject.SetActive(true);

        }
    }
}
