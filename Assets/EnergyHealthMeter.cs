using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyHealthMeter : MonoBehaviour 
{
    public Button EnergyButton;
    private float EnergyHealth;
    private const float MAX_ENERGY_HEALTH = 500.0f;
	void Start () 
    {
        if(GameManager.GetPlayerEnergy() == null || GameManager.GetPlayerEnergy() == -1f || GameManager.GetPlayerEnergy() == 0)
        {
            EnergyHealth = 500.0f;
        }
        else
        {
            EnergyHealth = GameManager.GetPlayerEnergy();
        }
	}
	void Update () 
    {
        EnergyButton.image.fillAmount = (EnergyHealth / MAX_ENERGY_HEALTH) * 1;
	}
    public void UseEnergy(float EnergyUsed)
    {
        if(EnergyHealth != 0)
        {
            EnergyHealth -= EnergyUsed;
        }
    }
    public void ReplenishEnergy(float EnergyReplinished)
    {
        if (EnergyHealth != MAX_ENERGY_HEALTH)
        {
            EnergyHealth += EnergyReplinished;
        }
    }
    public float GetEnergyHealth()
    {
        return EnergyHealth;
    }
    public void SetBrainEnergyHealth(float BrainEnergy)
    {
        EnergyHealth = BrainEnergy;
    }
}
