using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    public static int PlayerSelected;
    public static int GameWinner;
    public static float Timer;
    public static int Lives;
    public static float PlayerEnergy;
    public static List<GameObject> Synapses = new List<GameObject>();
	
    public static void SetSynapsesList(List<GameObject> SetTo)
    {
        Synapses = SetTo;
    }
    public static List<GameObject> GetSynapsesList()
    {
        return Synapses;
    }
    public static void SetPlayerEnergy(float Energy)
    {
        PlayerEnergy = Energy;
    }
    public static float GetPlayerEnergy()
    {
        return PlayerEnergy;
    }
    public static void SetLives(int NumberOfLives)
    {
        Lives = NumberOfLives;
    }
    public static int GetLives()
    {
        return Lives;
    }
    public static void SetGameTimerStart(float TimerStart)
    {
        Timer = TimerStart;
    }
    public static float GetTimerStartTime()
    {
        return Timer;
    }
    public static void SetGameWinner(int PlayerType)
    {
        GameWinner = PlayerType;
    }
    public static int GetGameWinner()
    {
        return GameWinner;
    }
    public static void SetPlayerSelected(int PlayerType)
    {
        PlayerSelected = PlayerType;
    }
    public static int GetPlayerSelected()
    {
        return PlayerSelected;
    }
    public static string Print()
    {
        string Print = "GameManager Stores:";
        Print +=    "\nPlayerLives: " + Lives +  
                    "\nSynapses.Count :" + Synapses.Count +
                    "\nPlayerEnergy :" + PlayerEnergy +
                    "\nPlayerSelected :" + PlayerSelected +
                    "\nGameWinner :" + GameWinner +
                    "\nTimerAt :" + Timer;
        return Print;
    }
}
