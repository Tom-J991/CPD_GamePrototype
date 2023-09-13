// Game Manager Script
// Currently manages mouse locking.
// by: Thomas Jackson
// date: 6/09/2023 12:10AM
// last modified: 6/09/2023 12:10AM

using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    void OnExit()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void OnEnter()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
