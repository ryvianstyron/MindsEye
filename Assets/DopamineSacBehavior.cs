using UnityEngine;
using System.Collections;

public class DopamineSacBehavior : MonoBehaviour 
{
    private const int INFECTED = 0;
    private const int HEALTHY = 1;

    public GameObject InfectedNeuronFab;
    public GameObject HealthyNeuronFab;

    BrainHealthMeter BrainHealthMeter;
    NeuronBehavior NeuronBehavior;

    public int DopamineSacNumber;

    void Start()
    {
        BrainHealthMeter = (BrainHealthMeter)GameObject.Find("BrainHealth").GetComponent<BrainHealthMeter>();
    }
    
    public void Burst()
    {
        Debug.Log("Sac Burst!");
        
        GameObject InfectedNeuron1;
        GameObject InfectedNeuron2;

        InfectedNeuron1 = (GameObject)Instantiate(InfectedNeuronFab, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        NeuronBehavior = (NeuronBehavior)InfectedNeuron1.GetComponent<NeuronBehavior>();
        NeuronBehavior.SetNeuronType(INFECTED);
        NeuronBehavior.SetSpawnedFromDopamineSacNumber(DopamineSacNumber);
        NeuronBehavior.SetNeuronNumber(1);

        InfectedNeuron2 = (GameObject)Instantiate(InfectedNeuronFab, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        NeuronBehavior = (NeuronBehavior)InfectedNeuron2.GetComponent<NeuronBehavior>();
        NeuronBehavior.SetNeuronType(INFECTED);
        NeuronBehavior.SetSpawnedFromDopamineSacNumber(DopamineSacNumber);
        NeuronBehavior.SetNeuronNumber(2);

        BrainHealthMeter.ApplyPermanentDamage(25);
        Destroy(gameObject);
    }
    public void Release()
    {
        Debug.Log("Sac Release!");
        GameObject HealthyNeuron1;
        GameObject HealthyNeuron2;
        
        HealthyNeuron1 = (GameObject)Instantiate(HealthyNeuronFab, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        NeuronBehavior = (NeuronBehavior)HealthyNeuron1.GetComponent<NeuronBehavior>();
        NeuronBehavior.SetNeuronType(HEALTHY);
        NeuronBehavior.SetSpawnedFromDopamineSacNumber(DopamineSacNumber);
        NeuronBehavior.SetNeuronNumber(1);

        HealthyNeuron2 = (GameObject)Instantiate(HealthyNeuronFab, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        NeuronBehavior = (NeuronBehavior)HealthyNeuron2.GetComponent<NeuronBehavior>();
        NeuronBehavior.SetNeuronType(HEALTHY);
        NeuronBehavior.SetSpawnedFromDopamineSacNumber(DopamineSacNumber);
        NeuronBehavior.SetNeuronNumber(2);

        BrainHealthMeter.ApplyPermanentBonus(25);
        Destroy(gameObject);
    }
}
