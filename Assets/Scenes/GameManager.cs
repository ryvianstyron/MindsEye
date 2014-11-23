using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    public static int PlayerSelected = -1;
    public static int GameWinner = -1;
    public static float Timer = -1f;
    public static int Lives = -1;
    public static float PlayerEnergy = -1;
	
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
                    "\nPlayerEnergy :" + PlayerEnergy +
                    "\nPlayerSelected :" + PlayerSelected +
                    "\nGameWinner :" + GameWinner +
                    "\nTimerAt :" + Timer;
        return Print;
    }
}
