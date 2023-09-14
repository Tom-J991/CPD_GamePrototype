// Player Movement Script
// by: Thomas Jackson
// date: 6/09/2023 9:30AM
// last modified: 6/09/2023 10:17AM

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    // Properties
    [Header("Player Movement Properties")]
    [Tooltip("Player Movement Speed.")]
    public float movementSpeed;

    // Variables
    Vector2 m_movementVector = Vector2.zero;
   
    // References
    Rigidbody m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>(); // Get reference to Rigidbody component.
    }

    void FixedUpdate()
    {
        // Player movement logic.
        Camera cam = Camera.main; // Camera must be tagged as MainCamera

        Vector3 moveVector = (m_movementVector.x * cam.transform.right + m_movementVector.y * cam.transform.forward).normalized * movementSpeed; // Creates movement vector based on camera direction.
        moveVector.y = 0.0f;

        m_rb.AddForce(moveVector * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    // New Input System
    void OnMove(InputValue _value)
    {
        m_movementVector = _value.Get<Vector2>();
    }
    public void AltOnMove(Vector2 _value)
    {
        m_movementVector = _value;
    }
    public void AltOnMove(float _value)
    {
        m_movementVector = new Vector2(_value, m_movementVector.y);
    }
}
