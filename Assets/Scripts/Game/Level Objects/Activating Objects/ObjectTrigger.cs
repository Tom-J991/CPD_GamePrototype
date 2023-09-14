// Object Trigger script
// by: Halen
// created: 14/09/2023
// last edited: 14/09/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    [Tooltip("List of Activatable Objects that are triggered by this object.")]
    public ActivatableObject[] objects;

    [Header("Animations")]
    [Tooltip("Plays when this trigger is activated.")]
    public Animation activate;
    [Tooltip("Plays when this trigger is deactivated.")]
    public Animation deactivate;

    [Tooltip("If the trigger is currently active or not.")]
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

    public void Toggle()
    {
        active = !active;
        
        foreach (ActivatableObject obj in objects)
        {
            obj.Toggle();
        }
    }

    public void Toggle(bool value)
    {
        active = value;
        
        foreach (ActivatableObject obj in objects)
        {
            obj.Toggle(value);
        }
    }
}
