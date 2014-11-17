using UnityEngine;
using System.Collections;

public class NeuronBehavior : MonoBehaviour 
{
    const int INFECTED = 0;
    const int HEALTHY = 1;

    public int NeuronType;
    private int NeuronHealth = 10;
    
    private HUDManager HUD;
    private Player Player;

    void Start()
    {
        HUD = (HUDManager)GameObject.Find("Camera").GetComponent<HUDManager>();
    }
    void OnCollisionEnter(Collision Collision)
    {
        if (Collision.gameObject.name.Contains("Player"))
        {
            Player = (Player)Collision.gameObject.GetComponent<Player>();
            if ((HUD.IsCurePlayerSelected() && NeuronType == INFECTED) || (HUD.IsDiseasePlayerSelected() && NeuronType == HEALTHY))
            {
                if (Player.GetLives() > 0)
                {
                    Player.SetLives(Player.GetLives() - 1);
                }
                HUD.UpdateLives();
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
}
