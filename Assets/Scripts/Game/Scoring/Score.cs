// Score Script
// by: Halen Finlay
// date: 06/09/2023
// last modified: 07/09/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [Tooltip("Tracks if the Player is currently running the level")]
    public bool playingLevel;

    [Header("Canvases")]
    [Tooltip("Reference to the TimerUI canvas in the scene.")]
    public TimerUI timerUI;
    [Tooltip("Reference to the ScoreUI canvas in the scene.")]
    public ScoreUI scoreUI;

    [Header("Level Details")]
    public float[] targetScores = { 300f, 200f, 100f };
    public float targetTime = 40f;

    [Header("Score Variables")]
    public float finalScore;
    public float timeBonus;
    public float speedBonus;
    public List<float> penalties;

    [Header("Active Variables")]
    [Tooltip("Time passed since the Player started the level.")]
    [Min(0)]
    [SerializeField] private float m_timer;
    public float timer
    {
        get { return m_timer; }
    }
    [Tooltip("Keeps track of the player's highest speed.")]
    [SerializeField] private float m_maxSpeed;
    [Tooltip("Keeps track of which obstacles the Player has hit.")]
    [SerializeField] private List<GameObject> m_hitObstacles;

    // Private variables
    private Rigidbody m_player;

    // Start is called before the first frame update
    void Start()
    {
        m_timer = 0;
        m_hitObstacles.Clear();
        m_player = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update timer if the player is playing the level
        if (playingLevel) m_timer += Time.deltaTime;

        // Update the player's highest reached speed if they have reached a new high
        if (m_player.velocity.magnitude > m_maxSpeed) m_maxSpeed = m_player.velocity.magnitude;
    }

    // Calculate the player's bonuses, penalties, and final score
    private void CalculateScore()
    {
        // Calculate penalties
        // If no obstacles were hit, provide a bonus score instead
        float totalPenalties = 0;
        if (m_hitObstacles.Count == 0) totalPenalties = -100f;
        else
        {
            foreach (GameObject hit in m_hitObstacles)
            {
                float penalty = hit.GetComponent<Obstacle>().penalty;
                penalties.Add(penalty);
                totalPenalties += penalty;
            }
        }

        // Calculate time bonus
        if (m_timer >= targetTime)
        {
            timeBonus = 0;
        }
        else
        {
            timeBonus = Mathf.Floor(1800 * Mathf.Pow(targetTime - m_timer, 2) / Mathf.Pow(targetTime, 2));
        }

        // Calculate speed bonus
        speedBonus = Mathf.Floor(m_maxSpeed * 10);

        // Calculate final score based on all bonuses and penalties
        finalScore = timeBonus + speedBonus - totalPenalties;
    }

    public void OnCollisionEnter(Collision other)
    {
        // If the object the Player collided with has the Obstacle component and is not
        // already in the list of collided objects, then add it to the list
        if (other.gameObject.GetComponent<Obstacle>())
        {
            if (m_hitObstacles.IndexOf(other.gameObject) < 0)
            {
                m_hitObstacles.Add(other.gameObject);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // Checks if the player has reached the goal
        if (other.gameObject.GetComponent<Goal>())
        {
            timerUI.gameObject.SetActive(false);
            CalculateScore();
            scoreUI.gameObject.SetActive(true);
        }
    }
}
