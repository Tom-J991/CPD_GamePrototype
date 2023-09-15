// Blade Spin script
// by: Halen
// created: 13/09/2023
// last edited: 13/09/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    // Front and back wind zones attached to this object
    private BoxCollider m_zone;

    [Header("Wind Zone Info")]
    [Tooltip("The width and height of the wind zone.")]
    [Min(0)]
    public float zoneSize = 6f;

    [Tooltip("How far in front of the wind source the wind zone extends.")]
    [Min(0)]
    public float zoneLength = 14f;

    [Tooltip("The amount of force the wind zone applies to objects.")]
    [Min(0)]
    public float windSpeed = 15f;

    [Tooltip("Which side of the wind source the zone is on.")]
    public bool backZone = false;

    // Start is called before the first frame update
    void Start()
    {
        m_zone = GetComponent<BoxCollider>(); // get the zone
        m_zone.isTrigger = true; // ensure zone is set to trigger

        // Set the parameters of the wind zones
        if (m_zone)
        {
            // set the size of the zone
            m_zone.size = new Vector3(zoneSize, zoneSize, zoneLength);
            // correctly set the position of the zone
            if (backZone) m_zone.transform.localPosition = new Vector3(0, 0, -m_zone.size.z / 2);
            else m_zone.transform.localPosition = new Vector3(0, 0, m_zone.size.z / 2);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Get the player's rigidbody
            Rigidbody player = other.gameObject.GetComponentInParent<Rigidbody>();
            // Scale the force based on the closeness to the wind source
            float distanceFactor = 1f - Vector3.Distance(transform.parent.transform.position, player.position) / (zoneLength + 1.5f);
            // Push the player away from the wind source
            player.AddForce(transform.parent.forward * windSpeed * distanceFactor, ForceMode.Acceleration);
        }
    }

    private void OnValidate()
    {
        if (m_zone)
        {
            // set the size of the zone
            m_zone.size = new Vector3(zoneSize, zoneSize, zoneLength);
            // correctly set the position of the zone
            if (backZone) m_zone.transform.localPosition = new Vector3(0, 0, -m_zone.size.z / 2);
            else m_zone.transform.localPosition = new Vector3(0, 0, m_zone.size.z / 2);
        }
    }
}
