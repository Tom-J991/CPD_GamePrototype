//Highscore script
//by Halen? Jackson
//Last Edited 20/9/23 4:30 PM

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Highscores : MonoBehaviour
{
    [System.Serializable]
    public class Score
    {
        public string name;
        public int score;
        public float time;
        public char rank;
    }
    public List<Score> highscores;

    public GameObject scoreBoard;
    public GameObject scoreEntryTemplate;
    //public List<Transform> entryFields;

    private void Awake()
    {
        scoreEntryTemplate.SetActive(false);
        foreach(Score score in highscores)
        {
            //Make a new entry and attach it to the board
            GameObject newEntry = Instantiate(scoreEntryTemplate);
            newEntry.transform.SetParent(scoreBoard.transform, false);
            //Get the entry fields
            TextMeshProUGUI[] textFields = newEntry.GetComponentsInChildren<TextMeshProUGUI>();
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
        }
    }
}
