using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class Player : MonoBehaviour 
{
	private int Lives;
    private int MaxHealth;

    private int CurrentLevel;

    private int Mana;
    private int MaxMana ;

    GameObject GameManager;
    List<Power> PickedUpPowers = new List<Power>();

    void Start()
    {
        Lives = 3;
        MaxHealth = 3;
        CurrentLevel = 1;
        Mana = 20;
        MaxMana = 100;
    }
    void Update()
    {
        if(Lives == 0)
        {
            Debug.Log("Player is dead!");
            Destroy(gameObject);
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
	public void SetMana(int Mana)
	{
		this.Mana = Mana;
	}
	public int GetMana()
	{
		return Mana;
	}
	public int GetLevel()
	{
		return CurrentLevel;
	}
	public void SetLevel(int Level)
	{
		this.CurrentLevel = Level;
	}
    public void AddToPowerList(Power Power)
    {
		//Debug.Log ("Player Class - Add To Power List");
        PickedUpPowers.Add(Power);
    }
    public List<Power> GetPowersCollected()
    {
        return PickedUpPowers;
    }
    public int CheckIfPowerExists(string PowerTag)
    {
        int ReturnIndex = -1;
        if (PickedUpPowers != null)
        {
            for(int i = 0; i < PickedUpPowers.Count; i++)
            {
                if(PickedUpPowers[i].GetPowerTag().Equals(PowerTag))
                {
                    ReturnIndex = i;
                }
            }
        }
        return ReturnIndex;
    }
}
