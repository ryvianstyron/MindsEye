using UnityEngine;
using System.Collections;

public class SynapseBehavior : MonoBehaviour 
{
    private const int UNHEALTHY = 0;
    private const int HEALTHY = 1;

    private int SynapseType;
    private float SynapseHealth; // 100 is full health
    private float HealthMultipler;

    public GameObject SynapseHealthBack; // white
    public GameObject SynapseHealthFront; // red

    public bool ShouldDamageCurePlayer;
    public bool ShouldDamageDiseasePlayer;

	void Start () 
    {
        int RandomHealth = Random.Range(25, 75);
        this.SynapseHealth = RandomHealth;
        if (RandomHealth >= 50)
        {
            this.SynapseType = HEALTHY;
        }
        else if(RandomHealth < 50)
        {
            this.SynapseType = UNHEALTHY;
        }
        switch (SynapseType)
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
    public float GetSynapseHealth()
    {
        return SynapseHealth;
    }
    public int GetSynapseType()
    {
        return SynapseType;
    }
    void Update()
    {
        switch (SynapseType)
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
    }
    public void Repair(float RepairBy)
    {
        if(!(SynapseHealth + RepairBy > 100))
        {
            SynapseHealth += RepairBy;
            HealthMultipler = (SynapseHealth / 100);
            SynapseHealthBack.transform.localScale = new Vector3(SynapseHealthFront.transform.localScale.x * HealthMultipler,
                                                                 SynapseHealthBack.transform.localScale.y,
                                                                 SynapseHealthBack.transform.localScale.z);
            if(SynapseHealth >= 50)
            {
                SynapseType = HEALTHY;
            }
            
        }
    }
    public void Damage(float DamageBy)
    {
        if(!(SynapseHealth - DamageBy < 0))
        {
            SynapseHealth -= DamageBy;
            HealthMultipler = (SynapseHealth / 100);
            SynapseHealthBack.transform.localScale = new Vector3(SynapseHealthFront.transform.localScale.x * HealthMultipler,
                                                                 SynapseHealthBack.transform.localScale.y,
                                                                 SynapseHealthBack.transform.localScale.z);
            if (SynapseHealth < 50)
            {
                SynapseType = UNHEALTHY;
            }
        }
    }
}
