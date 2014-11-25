using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour 
{
    HUDManager HUD;
    Player Player;
    // Use this for initialization
	void Start () 
    {
        HUD = (HUDManager)GameObject.Find("Camera").GetComponent<HUDManager>();
        StartCoroutine("DestroyBullet");
	}
	// Update is called once per frame
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
	void Update () 
    {
	    
	}
    void OnCollisionEnter(Collision Collision)
    {
        // Destroy bullet if it hits anything other than another player
        if (!(Collision.collider.gameObject.name.Contains("PlayerTwo")) || !(Collision.collider.gameObject.name.Contains("PlayerTwo"))) 
        {
            Destroy(gameObject);
        }
        else
        {
            if (HUD.IsCurePlayerSelected())
            {
                if (Collision.collider.gameObject.name.Contains("PlayerTwo"))
                {
                    Player = (Player)Collision.collider.gameObject.GetComponent<Player>();
                    if (Player.GetLives() > 0)
                    {
                        Player.SetLives(Player.GetLives() - 1);
                    }
                }
            }
            else if (HUD.IsDiseasePlayerSelected())
            {
                if (Collision.collider.gameObject.name.Contains("PlayerOne"))
                {
                    Player = (Player)Collision.collider.gameObject.GetComponent<Player>();
                    if (Player.GetLives() > 0)
                    {
                        Player.SetLives(Player.GetLives() - 1);
                    }
                }
            }
        }
    }
}
