using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SynapsesHolder : MonoBehaviour 
{
    // Column 1
    public GameObject SynapseCol1_1;
    public GameObject SynapseCol1_2;
    public GameObject SynapseCol1_3;
    
    // Column 2
    public GameObject SynapseCol2_1;
    public GameObject SynapseCol2_2;

    // Column 3
    public GameObject SynapseCol3_1;
    public GameObject SynapseCol3_2;

    // Column 4
    public GameObject SynapseCol4_1;
    public GameObject SynapseCol4_2;
    
    // Column 5
    public GameObject SynapseCol5_1;
    public GameObject SynapseCol5_2;
    public GameObject SynapseCol5_3;

    List<GameObject> Synapses = new List<GameObject>();
	// Use this for initialization
	void Awake () 
    {
            Synapses.Add(SynapseCol1_1);
            Synapses.Add(SynapseCol1_2);
            Synapses.Add(SynapseCol1_3);

            Synapses.Add(SynapseCol2_1);
            Synapses.Add(SynapseCol2_2);

            Synapses.Add(SynapseCol3_1);
            Synapses.Add(SynapseCol3_2);

            Synapses.Add(SynapseCol4_1);
            Synapses.Add(SynapseCol4_2);

            Synapses.Add(SynapseCol5_1);
            Synapses.Add(SynapseCol5_2);
            Synapses.Add(SynapseCol5_3);
    }
    public List<GameObject> GetAllSynapses()
    {
        return Synapses;
    }
}