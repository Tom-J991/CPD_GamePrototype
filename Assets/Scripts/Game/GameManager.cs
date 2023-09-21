// Game Manager Script
// Currently manages mouse locking.
// by: Thomas Jackson
// date: 6/09/2023 12:10AM
// last modified: 21/09/2023 4:02 PM by Jackson

using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("Objects that are only used on mobile devices.")]
    GameObject[] m_unqiueMobileObjects;
    [SerializeField, Tooltip("Objects that are only used on desktop.")]
    GameObject[] m_unqiueDesktopObjects;

    [SerializeField, Tooltip("En/Disables mouse locking")]
    bool m_doMouseLock;
    [SerializeField]
    bool m_ForceMobilePlatform = false;
    void Awake()
    {
        if (m_doMouseLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        PlatformCheck();
    }

    void OnExit()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void OnEnter()
    {
        if(m_doMouseLock)
            Cursor.lockState = CursorLockMode.Locked;
    }
    public void UnlockMouse()
    {
        m_doMouseLock = false;
        OnExit();
    }
    public void LockMouse()
    {
        m_doMouseLock = true;
        OnEnter();
    }

    public void PlatformCheck()
    {
        if (m_ForceMobilePlatform)
        {
            foreach (GameObject go in m_unqiueMobileObjects)
            {
                go.SetActive(true);
            }
            foreach (GameObject go in m_unqiueDesktopObjects)
            {
                go.SetActive(false);
            }
            return;
        }
        switch (SystemInfo.deviceType)
        {
            case DeviceType.Handheld:
                //enable mobile assests/settings
                foreach(GameObject go in m_unqiueMobileObjects)
                {
                    go.SetActive(true);
                }
                foreach (GameObject go in m_unqiueDesktopObjects)
                {
                    go.SetActive(false);
                }
                break;
            case DeviceType.Desktop:
                //enable desktop assests/setttings
                foreach (GameObject go in m_unqiueMobileObjects)
                {
                    go.SetActive(false);
                }
                foreach (GameObject go in m_unqiueDesktopObjects)
                {
                    go.SetActive(true);
                }
                break;
        }
    }
}
