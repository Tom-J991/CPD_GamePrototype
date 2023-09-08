// Score Script
// by: Halen Finlay
// date: 06/09/2023
// last modified: 07/09/2023

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public Score scoreComponent;

    [Header("Text Displays")]
    [Tooltip("Displays the final score.")]
    public TextMeshProUGUI finalScoreDisplay;
    [Tooltip("Shows the score gained from the player's time.")]
    public TextMeshProUGUI timeScoreDisplay;
    [Tooltip("Shows the bonus from the Player's highest speed reached.")]
    public TextMeshProUGUI speedScoreDisplay;
    [Tooltip("Displays the list of penalites the Player accrued from hitting Obstacles.")]
    public TextMeshProUGUI penaltyDisplay;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        SetDisplayDetails();
    }

    private void SetDisplayDetails()
    {
        finalScoreDisplay.text = "Final Score: " + scoreComponent.finalScore.ToString();
        timeScoreDisplay.text = "Time Bonus: " + scoreComponent.timeBonus.ToString();
        speedScoreDisplay.text = "Max Speed bonus: " + scoreComponent.speedBonus.ToString();
        string penaltyText = "Penalties:\n";
        if (scoreComponent.penalties.Count == 0) penaltyText = "No penalties! +100";
        foreach (float penalty in scoreComponent.penalties)
        {
            penaltyText += "-" + penalty + "\n";
        }
        penaltyDisplay.text = penaltyText;
    }
}
