using UnityEngine;
using System.Collections;

public class RechargeStationBehavior : MonoBehaviour 
{
    EnergyHealthMeter EnergyHealthMeter;

	// Use this for initialization
	void Start () 
    {
        EnergyHealthMeter = GameObject.Find("PlayerEnergy").GetComponent<EnergyHealthMeter>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    public void Recharge()
    {
       EnergyHealthMeter.ReplenishEnergy(1.0f);
    }
}
