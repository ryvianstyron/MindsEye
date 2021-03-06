﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// This script counts down from 3 min : 00 seconds  to 0 min : 00 seconds
public class TimerScript : MonoBehaviour 
{
    public Text TimerText;
    private float Timer;
    float millis;
    int minutes;
    int seconds;
    float totalseconds;
    bool GameOver = false;
    BrainHealthMeter BrainHealthMeter;

    void Start () 
    {
        BrainHealthMeter = GameObject.Find("BrainHealth").GetComponent<BrainHealthMeter>();
        if(GameManager.GetTimerStartTime() == -1f)
        {
            Timer = 181.0f; // 3 minutes x 60 seconds
        }
        else
        {
            Timer = GameManager.GetTimerStartTime();
        }
        totalseconds = Timer % 3600; 
        minutes = (int)totalseconds / 60;
        seconds = (int)totalseconds % 60;
        string sec = "" + seconds;
        string min = "" + minutes;
        if(min.Length == 1)
        {
            min = "0" + min;
        }
        if (sec.Length == 1)
        {
            sec = "0" + sec;
        }
        TimerText.text = "time " + min + ":" + sec;
	}
    public float GetCurrentTime()
    {
        return Timer;
    }
	void Update () 
    {
        if(!GameOver)
        { 
            Timer -= Time.deltaTime;
            totalseconds = Timer % 3600;
            minutes = (int)totalseconds / 60;
            seconds = (int)totalseconds % 60;
            string sec = "" + seconds;
            string min = "" + minutes;
            if (min.Length == 1)
            {
                min = "0" + min;
            }
            if(sec.Length == 1)
            {
                sec = "0" + sec;
            }
            TimerText.text = "time " + min + ":" + sec;
            if(minutes == 0 && seconds == 0)
            {
                Debug.Log("Getting into Game Over!");
                GameOver = true;
                GameManager.SetGameWinner(BrainHealthMeter.GetPlayerWhoWon());
                Application.LoadLevel("GameOverMenu");
            }
        }
	}
}
