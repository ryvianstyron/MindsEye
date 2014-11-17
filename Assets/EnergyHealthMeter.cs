using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyHealthMeter : MonoBehaviour 
{
    public Button EnergyButton;
    private float EnergyHealth = 100.0f;
    private const float MAX_ENERGY_HEALTH = 100.0f;
	void Start () 
    {
	
	}
	void Update () 
    {
        EnergyButton.image.fillAmount = (EnergyHealth / MAX_ENERGY_HEALTH) * 1;
	}
    public void UseEnergy(float EnergyUsed)
    {
        Debug.Log("UseEnergy<>EnergyHealth: " + EnergyHealth + ",EnergyUsed: " + EnergyUsed);
        if((EnergyHealth - EnergyUsed > 0))
        {
            EnergyHealth -= EnergyUsed;
        }
    }
    public void ReplenishEnergy(float EnergyReplinished)
    {
        if(!(EnergyHealth + EnergyReplinished > 100))
        {
            EnergyHealth += EnergyReplinished;
        }
    }
}
