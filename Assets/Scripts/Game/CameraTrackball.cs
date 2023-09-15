// Camera Trackball Script
// by: Thomas Jackson
// date: 6/09/2023 10:24AM
// last modified: 7/09/2023 10:54AM

using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera)), RequireComponent(typeof(CameraFollow))]
public class CameraTrackball : MonoBehaviour
{
    // Properties
    [Header("Orbit Properties")]
    [Tooltip("The speed at which the camera will rotate around the target.")]
    public float rotateSpeed = 1.0f;

    [SerializeField, Range(-89.0f, 89.0f)]
    [Tooltip("The minimum vertical angle the camera can look.")]
    float minVerticalAngle = -30.0f;

    [SerializeField, Range(-89.0f, 89.0f)]
    [Tooltip("The maximum vertical angle the camera can look.")]
    float maxVerticalAngle = 60.0f;

    // Mouse Input
    Vector2 m_mousePosition = Vector2.zero;
    bool m_mouseClicked = false;

    // Variables
    Vector2 m_orbitAngles = new Vector2(0.0f, 0.0f);

    public float InitialAngle = 0;

    void Awake()
    {
        // Update camera on script awake.
        Orbit();
    }

    void Update()
    {
        // Get Mouse Inputs.
        m_mousePosition = Mouse.current.delta.ReadValue();
        m_mouseClicked = Mouse.current.leftButton.ReadValue() >= 0.001f;
    }

    void FixedUpdate()
    {
        // Update camera in game.
        Orbit();
    }

    void Orbit()
    {
        Vector2 mouseInput = new Vector2(-m_mousePosition.y, m_mousePosition.x);

        // Trackball Logic.
        Quaternion lookRotation;

        // Rotate euler axis based on mouse movement.
        m_orbitAngles += mouseInput * rotateSpeed * Time.fixedDeltaTime;

        // Limit angles.
        m_orbitAngles.x = Mathf.Clamp(m_orbitAngles.x, minVerticalAngle, maxVerticalAngle); // Limits vertical angle between range.

        if (m_orbitAngles.y < 0.0f) // Wraps horizontal angle between 0 and 360 degrees.
            m_orbitAngles.y += 360.0f;
        if (m_orbitAngles.y >= 360.0f)
            m_orbitAngles.y -= 360.0f;

        lookRotation = Quaternion.Euler(m_orbitAngles); // Convert euler axis to quaternion rotation.

        transform.localRotation = lookRotation; // Updates camera rotation.
    }

    void OnValidate()
    {
        // Avoid maximum angle being below minimum when changing values in editor.
        if (maxVerticalAngle < minVerticalAngle)
            maxVerticalAngle = minVerticalAngle;
    }
}
