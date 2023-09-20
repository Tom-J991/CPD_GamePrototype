// Score Script
// by: Halen Finlay
// date: 06/09/2023
// last modified: 20/09/2023 12:34 AM by Jackson

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Score : MonoBehaviour
{
    [Tooltip("Tracks if the Player is currently running the level")]
    public bool playingLevel = true;

    [Header("Canvases")]
    [Tooltip("Reference to the TimerUI canvas in the scene.")]
    public TimerUI timerUI;
    [Tooltip("Reference to the ScoreUI canvas in the scene.")]
    public ScoreUI scoreUI;

    [Header("Level Details")]
    public float[] targetScores = { 300f, 200f, 100f };
    public float targetTime = 40f;
    public float targetSpeed = 20f;

    [Header("Score Variables")]
    public float finalScore;
    public float timeBonus;
    public float speedBonus;
    public List<float> penalties;
    public int ranking = -1;

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

    //~j
    public ParticleSystem m_victoryParticles;
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
                float penalty = 0;
                penalty = Mathf.Floor(Vector3.Magnitude(hit.GetComponent<MeshRenderer>().bounds.size) * 8f);
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

        if (timeBonus > 1000) timeBonus = 1000f;

        // Calculate speed bonus
        speedBonus = Mathf.Floor(m_maxSpeed / targetSpeed * 600);

        // Calculate final score based on all bonuses and penalties
        finalScore = timeBonus + speedBonus - totalPenalties;

        // Ranking system - probably could be improved
        for(int i = 0; i < targetScores.Length; i++)
        {
            if (finalScore >= targetScores[i])
            {
                ranking = i;
                break;
            }
        }
        if (ranking < 0)
        {
            if (finalScore > 0)
                ranking = 3;
            else
                ranking = 4;
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        // If the object the Player collided with has the Obstacle component and is not
        // already in the list of collided objects, then add it to the list
        if (other.gameObject.tag == "Obstacle")
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
        if (other.gameObject.tag == "Goal")
        {
            //timerUI.gameObject.SetActive(false);
            CalculateScore();
            MenuManager mm = FindObjectOfType<MenuManager>();
            int i = mm.GetIndexOfMenu(scoreUI.gameObject);
            if (i < 0)
                Debug.Log("The score ui has not been added to the menu manager or doesn't exist.\nPlease make sure that it is set up correctly.");
            else
            {
                FindObjectOfType<GameManager>().UnlockMouse();
                mm.SwitchToMenu(i);
                mm.m_doPause = false;
            }
            //scoreUI.gameObject.SetActive(true);
            ParticleSystem pfx = Instantiate(m_victoryParticles, transform.position, Quaternion.identity);
            playingLevel = false;
            gameObject.SetActive(false);
        }
    }
}
