using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BrainHealthMeter : MonoBehaviour 
{
    const float MAX_BRAIN_HEALTH = 1200f; 
    private float CurrentBrainHealth;
    public Button BrainFill;
   
    SynapsesHolder SynapsesHolder;
    List<GameObject> Synapses = new List<GameObject>();
	void Start () 
    {
        SynapsesHolder = (SynapsesHolder)GameObject.Find("Camera").GetComponent<SynapsesHolder>();
        Synapses = SynapsesHolder.GetAllSynapses();
	}
	
	// Update is called once per frame
    void Update()
    {
        CurrentBrainHealth = 0; // Reset on every tick
        foreach (GameObject Synapse in Synapses)
        {
            SynapseBehavior SynapseBehavior = (SynapseBehavior)Synapse.GetComponent<SynapseBehavior>();
            CurrentBrainHealth += SynapseBehavior.GetSynapseHealth();
        }
        BrainFill.image.fillAmount = (CurrentBrainHealth / MAX_BRAIN_HEALTH) * 1;
    }
}
