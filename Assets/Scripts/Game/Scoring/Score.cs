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
    public float targetTime = 10f;

    [Header("Score Variables")]
    public float finalScore;
    public float timeBonus;
    public List<float> penalties;

    [Header("Active Variables")]
    [Tooltip("Time passed since the Player started the level.")]
    [Min(0)]
    [SerializeField] private float m_timer;
    public float timer
    {
        get { return m_timer; }
    }

    [Tooltip("Keeps track of which obstacles the Player has hit.")]
    [SerializeField] private List<GameObject> m_hitObstacles;
    
    // Start is called before the first frame update
    void Start()
    {
        m_timer = 0;
        m_hitObstacles.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (playingLevel) m_timer += Time.deltaTime; ;
    }

    private void CalculateScore()
    {
        float totalPenalties = 0;
        foreach (GameObject hit in m_hitObstacles)
        {
            float penalty = hit.GetComponent<Obstacle>().penalty;
            penalties.Add(penalty);
            totalPenalties += penalty;
        }
        timeBonus = Mathf.Floor(targetTime / m_timer * 1000);

        finalScore = timeBonus - totalPenalties;
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
        if (other.gameObject.GetComponent<Goal>())
        {
            timerUI.gameObject.SetActive(false);
            CalculateScore();
            scoreUI.gameObject.SetActive(true);
        }
    }
}
