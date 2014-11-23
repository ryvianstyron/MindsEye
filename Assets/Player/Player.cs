using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class Player : MonoBehaviour 
{
    private TimerScript TimerScript;
	
    private int Lives;
    private int PlayerEnergy;

    private EnergyHealthMeter EnergyHealthMeter;
    private SynapsesHolder SynapsesHolder;
    void Start()
    {
        SynapsesHolder = (SynapsesHolder)GameObject.Find("Camera").GetComponent<SynapsesHolder>();
        EnergyHealthMeter = (EnergyHealthMeter)GameObject.Find("PlayerEnergy").GetComponent<EnergyHealthMeter>();
        TimerScript = (TimerScript)GameObject.Find("Timer").GetComponent<TimerScript>();
        if(GameManager.GetLives() == null || GameManager.GetLives() == -1)
        {
            Debug.Log("Player Start() - GameManager Stuff is Unset");
            Lives = 3;
        }
        else
        {
            Lives = GameManager.GetLives();
        }
    }
    public void RespawnPlayer(bool AllLivesLost)
    {
        Debug.Log("RespawnPlayer is being called");
        if(AllLivesLost) // If lives was 0 on death - Come back with only 1 life and 50% BrainEnergy
        {
            GameManager.SetLives(1);
            GameManager.SetPlayerEnergy(250.0f);
            GameManager.SetGameTimerStart(TimerScript.GetCurrentTime());
            GameManager.SetSynapsesList(SynapsesHolder.GetAllSynapses());
            Debug.Log(GameManager.Print());
        }
        else if(!AllLivesLost) // Bring him back with everything "intact"
        {
            GameManager.SetLives(GetLives());
            GameManager.SetPlayerEnergy(EnergyHealthMeter.GetEnergyHealth());
            GameManager.SetSynapsesList(SynapsesHolder.GetAllSynapses());
            GameManager.SetGameTimerStart(TimerScript.GetCurrentTime());
            Debug.Log(GameManager.Print());
        }
        Application.LoadLevel("Brain");
    }
    void Update()
    {
        if(Lives == 0)
        {
            RespawnPlayer(true);
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
