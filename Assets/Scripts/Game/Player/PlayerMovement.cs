// Player Movement Script
// by: Thomas Jackson
// date: 6/09/2023 9:30AM
// last modified: 21/09/2023 3:12 PM by Jackson

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    [Header("UI")]
    [Tooltip("Reference to the swing meter bar.")]
    public Image bar;

    [Tooltip("The canvas that displays the launch meter before swinging.")]
    public GameObject meterCanvas;

    // Variables
    bool m_hasBeenHit = true;

    Vector2 m_movementVector = Vector2.zero;
    bool m_isGrounded = false;

    bool m_onDrift = false;
    float m_boostMeter = 0.0f;

    float m_barRange;

    // References
    Rigidbody m_rb;
    AudioSource m_rollingSource;
    AudioSource m_collisionSource;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>(); // Get reference to Rigidbody component.
        // Get audio sources
        m_rollingSource = GetComponent<AudioSource>();
        m_collisionSource = GetComponentInChildren<Collider>().GetComponent<AudioSource>();

        m_barRange = (bar.transform.parent.GetComponent<RectTransform>().sizeDelta.y - 10 - bar.GetComponent<RectTransform>().sizeDelta.y / 2) / 2;
    }

    float boostInc = 0.0f;
    void FixedUpdate()
    {
        Camera cam = Camera.main; // Camera must be tagged as MainCamera

        // Physics logic.
        m_isGrounded = Physics.CheckSphere(groundCheck.position, 0.08f, groundMask); // Check if the ball is on the ground.

        // Tee off logic.


        if (!m_hasBeenHit) // Don't move or drift if ball has not been tee'd.
            return;

        // Drifting logic.
        if (m_onDrift && m_isGrounded)
        {
            /*
            // Builds up boost meter when button is held and ball is grounded.
            boostInc += boostIncrement * m_rb.velocity.magnitude/10f * Time.fixedDeltaTime;
            m_boostMeter += boostInc * Time.fixedDeltaTime;
            m_boostMeter = Mathf.Clamp(m_boostMeter, 0.0f, maxBoost);
            */
            m_boostMeter += boostIncrement * m_rb.velocity.magnitude * Time.fixedDeltaTime;
            m_boostMeter = Mathf.Clamp(m_boostMeter, 0.0f, maxBoost);

            meterCanvas.SetActive(true); // Enable Boost Meter.
            // Set position of bar meter.
            float ratio = m_boostMeter / maxBoost * 2.0f;
            float barPosition = m_barRange * ratio - m_barRange;
            bar.transform.localPosition = new Vector2(bar.transform.localPosition.x, barPosition);
        }

        if (!m_onDrift && m_boostMeter > 0.0f)
        {
            // Boosts towards camera's forward vector when button is let go and boost meter has charge.
            Vector3 boostVector = cam.transform.forward * m_boostMeter;
            boostVector.y = 0.0f; // Prevent boosting upwards.

            m_rb.AddForce(boostVector * Time.fixedDeltaTime, ForceMode.Impulse);

            m_boostMeter = 0.0f; // Reset boost meter.
            boostInc = 0.0f; // Reset boost value.
            meterCanvas.SetActive(false); // Disable Boost Meter.
        }

        // Player movement logic.
        if (!m_onDrift)
        {
            // Creates movement vector based on camera direction.
            Vector3 moveVector = (m_movementVector.x * cam.transform.right + m_movementVector.y * cam.transform.forward).normalized * movementSpeed;
            moveVector.y = 0.0f; // Prevent moving upwards.

            m_rb.AddForce(moveVector * Time.fixedDeltaTime, ForceMode.Impulse);
        }

        MovementAudio();
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
        m_movementVector = new Vector2(_value, 1);
	}
	
    void OnDrift(InputValue _value)
    {
        m_onDrift = _value.isPressed;
    }
    public void AltOnDrift(bool _value)
    {
        m_onDrift = _value;
    }

    void MovementAudio()
    {
        m_rollingSource.volume = m_rb.velocity.magnitude / movementSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // When the player hits a wall or obstacle, play the hit sound.
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Obstacle")
        {
            m_collisionSource.Play();
            m_collisionSource.volume = m_rb.velocity.magnitude / 28;
        }
    }
}
