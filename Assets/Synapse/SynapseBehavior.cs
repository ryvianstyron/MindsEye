using UnityEngine;
using System.Collections;

public class SynapseBehavior : MonoBehaviour 
{
    private const int UNHEALTHY= 0;
    private const int HEALTHY = 1;
    
    public int SynapseType;

    public float SynapseHealth; // 100
    private float HealthMultipler;

    public GameObject SynapseHealthBack; // white
    public GameObject SynapseHealthFront; // red

    public bool ShouldDamageCurePlayer;
    public bool ShouldDamageDiseasePlayer;

	void Start () 
    {
        switch(SynapseType)
        {
            case UNHEALTHY:
                ShouldDamageCurePlayer = true;
                ShouldDamageDiseasePlayer = false;
                break;
            case HEALTHY:
                ShouldDamageDiseasePlayer = true;
                ShouldDamageCurePlayer = false;
                break;
        }
        HealthMultipler = (SynapseHealth / 100);
      
        SynapseHealthBack.transform.localScale = new Vector3(SynapseHealthBack.transform.localScale.x * HealthMultipler,
                                                                SynapseHealthBack.transform.localScale.y,
                                                                SynapseHealthBack.transform.localScale.z);
	}
	void Update () 
    {
	}
    public void Repair(int RepairBy)
    {
        SynapseHealth += RepairBy;
    }
    public void Damage(int DamageBy)
    {
        SynapseHealth -= DamageBy;
    }
}
