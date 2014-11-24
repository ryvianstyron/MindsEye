using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BrainHealthMeter : MonoBehaviour 
{
    const float MAX_BRAIN_HEALTH = 1200f; 
    private float CurrentBrainHealth;
    public Image BrainFill;
   
    SynapsesHolder SynapsesHolder;
    List<GameObject> Synapses = new List<GameObject>();
	void Start () 
    {
        SynapsesHolder = (SynapsesHolder)GameObject.Find("Camera").GetComponent<SynapsesHolder>();
        Synapses = SynapsesHolder.GetAllSynapses();
	}
    void Update()
    {
        CurrentBrainHealth = 0; // Reset on every tick
        foreach (GameObject Synapse in Synapses)
        {
            SynapseBehavior SynapseBehavior = (SynapseBehavior)Synapse.GetComponent<SynapseBehavior>();
            CurrentBrainHealth += SynapseBehavior.GetSynapseHealth();
        }
        BrainFill.fillAmount = (CurrentBrainHealth / MAX_BRAIN_HEALTH) * 1;
    }
    public int GetPlayerWhoWon()
    {
        int PlayerWhoWon = -1;
        float BrainHealthPercentage = CurrentBrainHealth / MAX_BRAIN_HEALTH;
        if(BrainHealthPercentage <= 0.5f)
        {
            PlayerWhoWon = 0;
        }
        else if(BrainHealthPercentage > 0.5f)
        {
            PlayerWhoWon = 1;
        }
        Debug.Log("BrainHealth Percentage: " + BrainHealthPercentage + " PlayerWon: " + PlayerWhoWon);
        return PlayerWhoWon;
    }
}