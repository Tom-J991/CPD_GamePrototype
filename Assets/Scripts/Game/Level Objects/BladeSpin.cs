// Blade Spin script
// by: Halen
// created: 13/09/2023
// last edited: 13/09/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSpin : MonoBehaviour
{
    [Header("Ensure the blades are the first child of the object.")]
    [Tooltip("Mutliplier for how fast the blades spin.")]
    [Min(0)]
    public float spinSpeed = 40f;

    // Sets the axis for the blades to spin on
    private Vector3 m_rotationDirection = new Vector3(0, 1, 0);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(m_rotationDirection * spinSpeed * Time.deltaTime);
    }
}
