using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [Tooltip("Access to the level timer.")]
    public Score scoreComponent;
    
    [Tooltip("Displays the timer for the level.")]
    public TextMeshProUGUI timerDisplay;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        SetDisplayDetails();
    }

    private void SetDisplayDetails()
    {
        float timer = scoreComponent.timer;
        float minutes = Mathf.Floor(timer / 60);
        float seconds  = timer - minutes * 60;

        timerDisplay.text = minutes.ToString() + ":" + seconds.ToString("00.00");
    }
}
