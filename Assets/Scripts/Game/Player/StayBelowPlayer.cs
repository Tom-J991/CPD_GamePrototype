// Stay below Player Script
// by: Thomas Jackson
// date: 7/09/2023 12:06AM
// last modified: 7/09/2023 12:16AM

using UnityEngine;

[ExecuteInEditMode]
public class StayBelowPlayer : MonoBehaviour
{
    [Tooltip("The player's transform.")]
    [SerializeField]
    Transform playerTransform;

    // Work around to keep stuff below the ball based on the world's up vector rather than the parent's up vector.
    // This is useful for stuff such as the ground check that shouldn't be rotated with the parent.

    void Awake()
    {
        // Call on script load.
        AdjustPosition();
    }

    void Update()
    {
        // Call on game & editor update.
        AdjustPosition();
    }

    void AdjustPosition()
    {
        Vector3 newPosition = playerTransform.position - Vector3.up;
        transform.position = newPosition;
    }
}
