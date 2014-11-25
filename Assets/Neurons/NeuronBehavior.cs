using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NeuronBehavior : MonoBehaviour 
{
    private int NeuronNumber;
    private const int DISTANCE_AWAY = 1;
    private NavMeshAgent Agent;

    const int INFECTED = 0;
    const int HEALTHY = 1;

    public int NeuronType;
    private int NeuronLives = 3;
    
    private HUDManager HUD;
    private Player Player;

    private int SpawnedFromDopamineSacNumber;
    private WayPointsHolder WayPointsHolder;

    public int[] DestinationsForNeuron;

    private List<GameObject> WayPoints;
    private int TargetSet;
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        WayPointsHolder = (WayPointsHolder)GameObject.Find("Camera").GetComponent<WayPointsHolder>();
        HUD = (HUDManager)GameObject.Find("Camera").GetComponent<HUDManager>();
        WayPoints = WayPointsHolder.GetWayPoints();

        switch(SpawnedFromDopamineSacNumber)
        {
            case 1:
                if(NeuronNumber == 1)
                {
                    SetAgentDestinationAndLookAt(0);
                }
                else
                {
                    SetAgentDestinationAndLookAt(1);
                }
                break;
            case 2:
                if (NeuronNumber == 1)
                {
                    SetAgentDestinationAndLookAt(2);
                }
                else
                {
                    SetAgentDestinationAndLookAt(3);
                }
                break;
            case 3:
                if (NeuronNumber == 1)
                {
                    SetAgentDestinationAndLookAt(4);
                }
                else
                {
                    SetAgentDestinationAndLookAt(5);
                }
                break;
            case 4:
                if (NeuronNumber == 1)
                {
                    SetAgentDestinationAndLookAt(6);
                }
                else
                {
                    SetAgentDestinationAndLookAt(7);
                }
                break;
            case 5:
                if (NeuronNumber == 1)
                {
                    SetAgentDestinationAndLookAt(8);
                }
                else
                {
                    SetAgentDestinationAndLookAt(9);
                }
                break;
            case 6:
                if (NeuronNumber == 1)
                {
                    SetAgentDestinationAndLookAt(10);
                }
                else
                {
                    SetAgentDestinationAndLookAt(11);
                }
                break;
        }
    }
    void Update()
    {
        switch (SpawnedFromDopamineSacNumber)
        {
            case 1:
                if (Vector3.Distance(transform.position, WayPoints[0].transform.position) <= DISTANCE_AWAY)
                {
                    SetAgentDestinationAndLookAt(1);
                }
                else if (Vector3.Distance(transform.position, WayPoints[1].transform.position) <= DISTANCE_AWAY)
                {
                    SetAgentDestinationAndLookAt(0);
                }
                break;
            case 2:
                if (Vector3.Distance(transform.position, WayPoints[2].transform.position) <= DISTANCE_AWAY)
                {
                    SetAgentDestinationAndLookAt(3);
                }
                else if (Vector3.Distance(transform.position, WayPoints[3].transform.position) <= DISTANCE_AWAY)
                {
                    SetAgentDestinationAndLookAt(2);
                }
                break;
            case 3:
                if (Vector3.Distance(transform.position, WayPoints[4].transform.position) <= DISTANCE_AWAY)
                {
                    SetAgentDestinationAndLookAt(5);
                }
                else if (Vector3.Distance(transform.position, WayPoints[5].transform.position) <= DISTANCE_AWAY)
                {
                    SetAgentDestinationAndLookAt(4);
                }
                break;
            case 4:
                if (Vector3.Distance(transform.position, WayPoints[6].transform.position) <= DISTANCE_AWAY)
                {
                    SetAgentDestinationAndLookAt(7);
                }
                else if (Vector3.Distance(transform.position, WayPoints[7].transform.position) <= DISTANCE_AWAY)
                {
                    SetAgentDestinationAndLookAt(6);
                }
                break;
            case 5:
                if (Vector3.Distance(transform.position, WayPoints[8].transform.position) <= DISTANCE_AWAY)
                {
                    SetAgentDestinationAndLookAt(9);
                }
                else if (Vector3.Distance(transform.position, WayPoints[9].transform.position) <= DISTANCE_AWAY)
                {
                    SetAgentDestinationAndLookAt(8);
                }
                break;
            case 6:
                if (Vector3.Distance(transform.position, WayPoints[10].transform.position) <= DISTANCE_AWAY)
                {
                    SetAgentDestinationAndLookAt(11);
                }
                else if (Vector3.Distance(transform.position, WayPoints[11].transform.position) <= DISTANCE_AWAY)
                {
                    SetAgentDestinationAndLookAt(10);
                }
                break;
        }
    }
    public void SetAgentDestinationAndLookAt(int index)
    {
        Agent.SetDestination(WayPoints[index].transform.position);
        transform.LookAt(WayPoints[index].transform.position);
    }
    void OnTriggerEnter(Collider Collider)
    {
        if(Collider.gameObject.name.Contains("Player"))
        {
            Player = (Player)Collider.gameObject.GetComponent<Player>();
            if ((HUD.IsCurePlayerSelected() && NeuronType == INFECTED) || (HUD.IsDiseasePlayerSelected() && NeuronType == HEALTHY))
            {
                if (Player.GetLives() > 0)
                {
                    Collider.gameObject.animation.Play("xhit_body");
                    Player.SetLives(Player.GetLives() - 1);
                }
            }
        }
        else if(Collider.gameObject.name.Contains("EnergyBullet"))
        {
            if(NeuronType == INFECTED && HUD.IsCurePlayerSelected() || NeuronType == HEALTHY && HUD.IsDiseasePlayerSelected())
            {
                if (NeuronLives > 0)
                {
                    SetNeuronLives(GetNeuronLives() - 1);
                }
                if (NeuronLives == 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    public void SetNeuronType(int Type)
    {
        NeuronType = Type;
    }
    public int GetNeuronType()
    {
        return NeuronType;
    }
    public void SetSpawnedFromDopamineSacNumber(int DopamineSacNumber)
    {
        SpawnedFromDopamineSacNumber = DopamineSacNumber;
    }
    public int GetSpawnedFromDopamineSacNumber()
    {
        return SpawnedFromDopamineSacNumber;
    }
    public void SetNeuronNumber(int NeuronNumber)
    {
        this.NeuronNumber = NeuronNumber;
    }
    public int GetNeuronLives()
    {
        return NeuronLives;
    }
    public void SetNeuronLives(int Lives)
    {
        NeuronLives = Lives;
    }
}
