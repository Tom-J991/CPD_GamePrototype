// Camera Follow Script
// by: Thomas Jackson
// date: 6/09/2023 9:52AM
// last modified: 6/09/2023 1:45PM

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    // Properties
    [Header("Follow Target")]
    [Tooltip("The target's transform which the camera will follow.")]
    public Transform targetObject;

    [Header("Follow Properties")]
    [Tooltip("The speed at which the camera moves towards the targets position.")]
    public float followSpeed = 1.0f;

    [SerializeField]
    [Tooltip("The default distance to offset the camera from the target's transform.")]
    float distance = 1.0f;

    [Header("Zoom Properties")]
    [SerializeField]
    [Tooltip("The speed at which the camera zooms into the target.")]
    float scrollSpeed = 1.0f;

    [SerializeField, Range(-64.0f, 0.0f)]
    float minZoom = -6.4f;
    [SerializeField, Range(0.0f, 8.0f)]
    float maxZoom = 6.4f;

    [HideInInspector] public Vector3 followPosition;

    // Variables
    float m_targetZoom = 0.0f;

    void Update()
    {
        // Zoom into target logic.
        m_targetZoom += Mouse.current.scroll.ReadValue().y * scrollSpeed * Time.deltaTime;
        m_targetZoom = Mathf.Clamp(m_targetZoom, minZoom, maxZoom);
    }

    void FixedUpdate()
    {
        // Follow logic.
        followPosition = targetObject.position - (transform.forward * (distance - m_targetZoom));
        transform.position = Vector3.Lerp(transform.position, followPosition, followSpeed * Time.fixedDeltaTime);
    }
}
