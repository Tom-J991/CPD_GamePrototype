// Move to Points script
// by: Halen
// created: 14/09/2023
// last edited: 14/09/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoints : MonoBehaviour
{
    [Tooltip("List of points for the object to move between.")]
    public Transform[] points;

    [Tooltip("The speed at which the object moves between points.")]
    [Min(0f)]
    public float moveSpeed = 5f;

    [Tooltip("Toggles whether the object should be moving.")]
    public bool active = true;

    // keeps track of which point the object is at
    private int m_index = 0;
    // holds the length of 'points'
    private int m_length;
    
    // Start is called before the first frame update
    void Start()
    {
        m_index = 0;
        m_length = points.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[m_index].position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, points[m_index].position) <= moveSpeed * Time.deltaTime)
            {
                m_index++;
                if (m_index == m_length) m_index = 0;
            }
        }
    }
}
