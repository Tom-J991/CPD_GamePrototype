// Activatable Object script
// by: Halen
// created: 14/09/2023
// last edited: 14/09/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableObject : MonoBehaviour
{
    [Tooltip("Whether the object is active or not.")]
    [SerializeField] private bool m_active;
    public bool active
    {
        get
        {
            return m_active;
        }
        set
        {
            // return if the value is unchanged
            if (m_active == value) return;

            // Play the correct animation
            m_active = value;
            if (m_active)
                if (activate) activate.Play();
            else
                if (deactivate) deactivate.Play();
        }
    }

    [Header("Animations")]
    [Tooltip("Plays when this object is activated.")]
    public Animation activate;
    [Tooltip("Plays when this object is deactivated.")]
    public Animation deactivate;

    public void Toggle()
    {
        active = !active;
    }

    public void Toggle(bool value)
    {
        active = value;
    }
}
