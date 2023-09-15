// Club Swing script
// by: Halen
// created: 14/09/2023
// last edited: 14/09/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClubSwing : MonoBehaviour
{
    [Tooltip("The canvas that displays the launch meter before swinging.")]
    public GameObject meterCanvas;
    
    [Tooltip("The arrow that shows the direction the player is going to launch in.")]
    public GameObject launchArrow;
    
    [Header("UI")]
    [Tooltip("Reference to the swing meter bar.")]
    public Image bar;

    [Header("Properties")]
    [Tooltip("Maximum amount of force that can be applied to the player.")]
    [Min(0f)]
    public float maxSwingForce;
    [Tooltip("The default angle when preparing to swing.")]
    [Range(0f, 90f)]
    public float defaultLaunchAngle;

    [Header("Debugging")]
    [Tooltip("Amount of force to be applied to the player on hit.")]
    [Min(0f)]
    [SerializeField] private float m_swingForce;
    [Tooltip("Launch angle of the player in degrees.")]
    [Range(-0f, 90f)]
    [SerializeField] private float m_launchAngle;

    // reference to the player
    private Rigidbody m_player;

    // the min/max range of the bar's y local coordinate
    private float m_barRange;

    // time at the start
    private float m_startTime;

    public enum SwingState
    {
        Ready = 0,
        Swinging = 1,
        Complete = 2
    };
    public static SwingState state = SwingState.Ready;
    
    // Start is called before the first frame update
    void Start()
    {
        // get player rigidbody and disallow them from moving
        m_player = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        m_player.isKinematic = true;

        // set the default launch angle
        m_launchAngle = 90f - defaultLaunchAngle;

        // set to starting state for the swing
        state = SwingState.Ready;

        // get the starting time
        m_startTime = Time.time;

        // Get the range of the launch meter bar for the UI
        m_barRange = (bar.transform.parent.GetComponent<RectTransform>().sizeDelta.y - 10 - bar.GetComponent<RectTransform>().sizeDelta.y / 2) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case SwingState.Ready:
                // get time since level start
                float time = Time.time - m_startTime;

                // calc swing force
                m_swingForce = maxSwingForce * Mathf.Asin(Mathf.Sin(2f * Mathf.PI * time - Mathf.PI / 2)) + Mathf.PI / 2 * maxSwingForce;

                // Set the position of the meter bar
                float ratio = m_swingForce / (Mathf.PI / 2 * maxSwingForce);
                float barPosition = m_barRange * ratio - m_barRange;
                bar.transform.localPosition = new Vector2(bar.transform.localPosition.x, barPosition);

                // changing launch angle
                if (Input.GetKey(KeyCode.W))
                {
                    m_launchAngle += Time.deltaTime * 13f;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    m_launchAngle -= Time.deltaTime * 13f;
                }
                m_launchAngle = Mathf.Clamp(m_launchAngle, 0, 90f);

                // set the position and rotation of the launch arrow
                launchArrow.transform.position = new Vector3(m_player.position.x,
                                                             2.5f * Mathf.Cos(m_launchAngle * Mathf.PI / 180) + m_player.position.y,
                                                             2.5f * Mathf.Sin(m_launchAngle * Mathf.PI / 180) + m_player.position.z);
                launchArrow.transform.rotation = Quaternion.Euler(new Vector3(m_launchAngle, 0, 0));

                // When player wants to swing
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    launchArrow.gameObject.SetActive(false);
                    state = SwingState.Swinging;
                }
                break;

            case SwingState.Swinging:
                // enable the player's rigidbody
                m_player.isKinematic = false;

                // rotate the player to face the direction they want to launch
                m_player.rotation = Quaternion.Euler(new Vector3(m_launchAngle - 90, 0, 0));

                // add launch force
                m_player.AddRelativeForce(m_player.transform.forward * m_swingForce, ForceMode.Impulse);

                // stop swinging
                state = SwingState.Complete;
                break;

            case SwingState.Complete:
                launchArrow.gameObject.SetActive(false);
                meterCanvas.gameObject.SetActive(false);

                break;
        }
    }
}
