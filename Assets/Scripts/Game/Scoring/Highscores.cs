//Highscore script
//by Halen & Jackson
//Last Edited 22/9/23 11:00 AM

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class HighScore
{
    public string name;
    public int score;
    public float time;
    public char rank;
}

public class Highscores : MonoBehaviour
{
    public List<HighScore> highscores;

    public GameObject scoreBoard;
    public GameObject scoreEntryTemplate;

    bool scoresLoaded = false;
    LevelJSONLoader JSONscores;
    void Start()
    {
        JSONscores = FindObjectOfType<LevelJSONLoader>();
    }
    void Update()
    {
        if (!scoresLoaded)
        {
            HighScore[] scores = JSONscores.data.highscores;
            if (scores.Length != 10)
            {
                Debug.LogError("Highscores not yet loaded!");
                return;
            }
            else
            {

                scoreEntryTemplate.SetActive(false);
                foreach (HighScore score in scores)
                {
                    //Make a new entry and attach it to the board
                    GameObject newEntry = Instantiate(scoreEntryTemplate);
                    newEntry.transform.SetParent(scoreBoard.transform, false);
                    //Get the entry fields
                    TextMeshProUGUI[] textFields = newEntry.GetComponentsInChildren<TextMeshProUGUI>();
                    if (textFields.Length == 4)
                    {
                        //Assign the name, score, time and rank to the respective fields
                        textFields[0].text = score.name;
                        textFields[1].text = score.score.ToString();
                        //Calculate time
                        float timer = score.time;
                        float minutes = Mathf.Floor(timer / 60);
                        float seconds = timer - minutes * 60;
                        textFields[2].text = minutes.ToString() + ":" + seconds.ToString("00.00");
                        textFields[3].text = "RANK " + score.rank;
                        //Enable the entry
                        newEntry.SetActive(true);
                        scoresLoaded = true;
                    }
                    else
                    {
                        Debug.Log("Not all score fields found. :(");
                    }
                }
            }
        }
    }
}
