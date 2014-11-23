using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PickUp : MonoBehaviour 
{
    public int PickUpType;
    public int PickUpAmount;
    GameObject CameraGameObject;
    Player Player;
    HUDManager HUD;

    public GameObject Cure;
    public GameObject Disease;

    void Start ()
    {
        CameraGameObject = GameObject.Find("Camera");
        HUD = (HUDManager)CameraGameObject.GetComponent(typeof(HUDManager));
        if(HUD.IsCurePlayerSelected())
        {
            Player = (Player)Cure.GetComponent(typeof(Player));
        }
        else if(HUD.IsDiseasePlayerSelected())
        {
            Player = (Player)Disease.GetComponent(typeof(Player));
        }
    }
}
