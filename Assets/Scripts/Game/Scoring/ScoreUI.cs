using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public Score scoreComponent;

    [Header("Text Displays")]
    [Tooltip("Displays the timer for the level.")]
    public TextMeshProUGUI timerDisplay;
    [Tooltip("Displays the final score.")]
    public TextMeshProUGUI finalScoreDisplay;
    [Tooltip("Shows the score gained from the player's time.")]
    public TextMeshProUGUI timeScoreDisplay;
    [Tooltip("Displays the list of penalites the Player accrued from hitting Obstacles.")]
    public TextMeshProUGUI penaltyDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
