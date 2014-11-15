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
    public void ApplyPickupToPlayer()
    {
        int PlayerHealth = Player.GetLives();
        int PlayerMana = Player.GetMana();

        /*if(PickUpType == 0) // Health
        {
            if (PlayerHealth + PickUpAmount >= MaxPlayerHealth)
            {
                Player.SetHealth(MaxPlayerHealth);
            }
            else
            {
                Player.SetHealth(PlayerHealth + PickUpAmount);
            }
            HUD.UpdateHealthBarOnScreen();
        }
        else if(PickUpType == 1) // Mana
        {
            if (PlayerMana + PickUpAmount >= MaxPlayerMana)
            {
                Player.SetMana(MaxPlayerMana);
            }
            else
            {
                Player.SetMana(PlayerMana + PickUpAmount);
            }
            HUD.UpdateManaBarOnScreen();
        }*/
        Destroy(gameObject);
    }
}
