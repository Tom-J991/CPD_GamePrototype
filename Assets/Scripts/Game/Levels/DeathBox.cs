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
            Destroy(other.gameObject);
            FindObjectOfType<GameManager>().UnlockMouse();
            deathCanvas.gameObject.SetActive(true);
        }
    }
}
