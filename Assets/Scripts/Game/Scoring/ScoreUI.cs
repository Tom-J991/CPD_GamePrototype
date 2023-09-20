// Score Script
// by: Halen Finlay
// date: 06/09/2023
// last modified: 20/09/2023 12:41 AM by Jackson

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [Tooltip("The image that displays the users rank upon level completion.")]
    public Image rankDisplay;
    public Sprite[] rankImages;
    public TextMeshProUGUI tauntTextBox;
    [Tooltip("Short messages to show based on the player's rank.")]
    public string[] rankTaunts;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
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
        finalScoreDisplay.text = scoreComponent.finalScore.ToString();
        timeScoreDisplay.text = scoreComponent.timeBonus.ToString();
        speedScoreDisplay.text = scoreComponent.speedBonus.ToString();
        float penaltyScore = 0;
        foreach (float penalty in scoreComponent.penalties)
        {
            penaltyScore -= penalty;
        }
        penaltyDisplay.text = (penaltyScore >= 0 ? "+100" : penaltyScore.ToString());

        rankDisplay.sprite = rankImages[scoreComponent.ranking];
        tauntTextBox.text = rankTaunts[scoreComponent.ranking];
    }
}
