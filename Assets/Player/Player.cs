using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class Player : MonoBehaviour 
{
    const int DISEASE = 0;
    const int CURE = 1;
    private TimerScript TimerScript;
	
    private int Lives;
    private int PlayerEnergy;

    private EnergyHealthMeter EnergyHealthMeter;
    private BrainHealthMeter BrainHealthMeter;
    private HUDManager HUD;
    void Start()
    {
        HUD = (HUDManager)GameObject.Find("Camera").GetComponent<HUDManager>();
        BrainHealthMeter = (BrainHealthMeter)GameObject.Find("BrainHealth").GetComponent<BrainHealthMeter>();
        EnergyHealthMeter = (EnergyHealthMeter)GameObject.Find("PlayerEnergy").GetComponent<EnergyHealthMeter>();
        TimerScript = (TimerScript)GameObject.Find("Timer").GetComponent<TimerScript>();
        if(GameManager.GetLives() == -1)
        {
            Lives = 3;
        }
        else
        {
            Lives = GameManager.GetLives();
        }
    }
    public void RespawnPlayer()
    {
        GameManager.SetLives(GetLives());
        GameManager.SetPlayerEnergy(EnergyHealthMeter.GetEnergyHealth());
        GameManager.SetGameTimerStart(TimerScript.GetCurrentTime());
        Debug.Log(GameManager.Print());
        if (GameManager.GetPlayerSelected() == DISEASE)
        {
            Destroy(GameObject.Find("PlayerTwo_Disease(Clone)"));
        }
        else if (GameManager.GetPlayerSelected() == CURE)
        {
            Destroy(GameObject.Find("PlayerOne_Cure(Clone)"));
        }
        HUD.RespawnPlayerInWorld();
    }
    void Update()
    {
        if(Lives == 0)
        {
            Debug.Log("Getting into Game Over!");
            GameManager.SetGameWinner(BrainHealthMeter.GetPlayerWhoWon());
            Application.LoadLevel("GameOverMenu");
        }
    }
	public void SetLives(int LivesToSet)
	{
        Lives = LivesToSet;
	}
	public int GetLives()
	{
        return Lives;
	}
}
