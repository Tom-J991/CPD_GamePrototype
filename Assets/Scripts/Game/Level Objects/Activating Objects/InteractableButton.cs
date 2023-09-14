// Ineractable Button script
// by: Halen
// created: 14/09/2023
// last edited: 14/09/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : ObjectTrigger
{
    [Header("Details")]
    [Tooltip("The range at which the player can interact with the button.")]
    public float range = 3f;

    [Tooltip("Whether a button will stay pressed after it has been pressed once.")]
    public bool permanent = false;

    // player's transform
    private Transform m_player;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position, m_player.position) <= range)
        {
            if (permanent) Toggle(true);
            else Toggle();
        }
    }
}
