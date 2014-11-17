using UnityEngine;
using System.Collections;

public class DopamineSacBehavior : MonoBehaviour 
{
    public GameObject InfectedNeuronFab;
    public GameObject HealthyNeuronFab;

    BrainHealthMeter BrainHealthMeter;
    void Start()
    {
        BrainHealthMeter = (BrainHealthMeter)GameObject.Find("BrainHealth").GetComponent<BrainHealthMeter>();
    }
    
    public void Burst()
    {
        Debug.Log("Sac Burst!");
        for(int i = 0; i < 5; i++)
        {
            GameObject InfectedNeuron = (GameObject)Instantiate(InfectedNeuronFab, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        }
        //BrainHealthMeter.
        Destroy(gameObject);
    }
    public void Release()
    {
        Debug.Log("Sac Release!");
        for (int i = 0; i < 5; i++)
        {
            GameObject HealthyNeuron = (GameObject)Instantiate(HealthyNeuronFab, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
