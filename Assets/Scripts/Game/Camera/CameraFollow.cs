// Camera Follow Script
// by: Thomas Jackson
// date: 6/09/2023 9:52AM
// last modified: 7/09/2023 10:54AM

using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    // Properties
    [Header("Follow Target")]
    [Tooltip("The target's transform which the camera will follow.")]
    public Transform targetObject;

    [SerializeField]
    [Tooltip("The default distance to offset the camera from the target's transform.")]
    float distance = 1.0f;

    [SerializeField]
    [Tooltip("The default height to offset the camera from the target's transform.")]
    float height = 0.0f;

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

    void Awake()
    {
        // Update camera's position on script awake.
        FollowTarget();
    }

    void FixedUpdate()
    {
        // Update camera's position in game.
        FollowTarget();
    }

    void OnValidate()
    {
        // Update camera's position on value change.
        // This allows the camera to be updated automatically in edit mode.
        FollowTarget();
    }

    // Follow logic.
    void FollowTarget()
    {
        // Find new camera position by taking the target's position and adding a distance offset based on the camera's forward vector
        // and then adding a height offset based on the camera's up vector.
        followPosition = targetObject.position - (transform.forward * (distance - m_targetZoom)) + (transform.up * height);
        transform.position = followPosition;
    }
}
