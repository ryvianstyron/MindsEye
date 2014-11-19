using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    public static int PlayerSelected;
    public static int GameWinner;
	
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
}
