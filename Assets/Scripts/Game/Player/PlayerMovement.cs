// Player Movement Script
// by: Thomas Jackson
// date: 6/09/2023 9:30AM
// last modified: 7/09/2023 11:21AM

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    // Properties
    [Header("Player Movement Properties")]
    [Tooltip("Player Movement Speed.")]
    public float movementSpeed = 12.0f;

    [Tooltip("The transform to test if the ball is grounded.")]
    [SerializeField]
    Transform groundCheck;

    [Tooltip("The layer mask that contains what is ground.")]
    [SerializeField]
    LayerMask groundMask;

    [Header("Drifting Properties")]
    public float maxBoost = 1024.0f;
    public float boostIncrement = 1024.0f;

    // Variables
    bool m_hasBeenHit = true;

    Vector2 m_movementVector = Vector2.zero;
    bool m_isGrounded = false;

    bool m_onDrift = false;
    float m_boostMeter = 0.0f;
   
    // References
    Rigidbody m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>(); // Get reference to Rigidbody component.
    }

    void FixedUpdate()
    {
        Camera cam = Camera.main; // Camera must be tagged as MainCamera

        // Physics logic.
        m_isGrounded = Physics.CheckSphere(groundCheck.position, 0.01f, groundMask); // Check if the ball is on the ground.

        // Tee off logic.


        if (!m_hasBeenHit) // Don't move or drift if ball has not been tee'd.
            return;

        // Drifting logic.
        if (m_onDrift && m_isGrounded)
        {
            // Builds up boost meter when button is held and ball is grounded.
            m_boostMeter += boostIncrement * Time.fixedDeltaTime;
            m_boostMeter = Mathf.Clamp(m_boostMeter, 0.0f, maxBoost);
        }

        if (!m_onDrift && m_boostMeter > 0.0f)
        {
            // Boosts towards camera's forward vector when button is let go and boost meter has charge.
            Vector3 boostVector = cam.transform.forward * m_boostMeter;
            boostVector.y = 0.0f; // Prevent boosting upwards.

            m_rb.AddForce(boostVector * Time.fixedDeltaTime, ForceMode.Impulse);

            m_boostMeter = 0.0f; // Reset boost meter.
        }

        // Player movement logic.
        if (!m_onDrift)
        {
            // Creates movement vector based on camera direction.
            Vector3 moveVector = (m_movementVector.x * cam.transform.right + m_movementVector.y * cam.transform.forward).normalized * movementSpeed;
            moveVector.y = 0.0f; // Prevent moving upwards.

            m_rb.AddForce(moveVector * Time.fixedDeltaTime, ForceMode.Impulse);
        }
    }

    private void OnGUI()
    {
        if (Application.isEditor)
        {
            GUI.Label(new Rect(0, 0, 800, 600), (m_boostMeter).ToString());
        }
    }

    // New Input System
    void OnMove(InputValue _value)
    {
        m_movementVector = _value.Get<Vector2>();
    }

    void OnDrift(InputValue _value)
    {
        m_onDrift = _value.isPressed;
    }
}
