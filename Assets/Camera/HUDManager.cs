using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class HUDManager : MonoBehaviour
{
    const int DISEASE = 0;
    const int CURE = 1;

    public Text LivesText;

    public GameObject Cure;
    public GameObject Disease;

    private Player Player;

    private bool CurePlayerSelected = false;
    private bool DiseasePlayerSelected = false;

    public GameObject Camera;

    private CameraFollower CameraFollowScript;
    private PlayerMovement MovementScript;

    private PlayerSpawner PlayerSpawner;

    void Update()
    {
        LivesText.text = "lives " + Player.GetLives();
    }
    void Start()
    {
        PlayerSpawner = Camera.GetComponent<PlayerSpawner>();
        CameraFollowScript = Camera.GetComponent<CameraFollower>();

        if (GameManager.GetPlayerSelected() == DISEASE)
        {
            DiseasePlayerSelected = true;
            Vector3 PositionToSpawn = PlayerSpawner.GetRandomSpawnPoint();
            PositionToSpawn.y += 3;
            GameObject PlayerDisease = (GameObject)Instantiate(Disease, PositionToSpawn, Quaternion.identity);
            Player = (Player)PlayerDisease.GetComponent(typeof(Player));
            CameraFollowScript.SetUpCamera(PlayerDisease);

        }
        else if (GameManager.GetPlayerSelected() == CURE)
        {
            CurePlayerSelected = true;
            Vector3 PositionToSpawn = PlayerSpawner.GetRandomSpawnPoint();
            PositionToSpawn.y += 3;
            GameObject PlayerCure = (GameObject)Instantiate(Cure, PositionToSpawn, Quaternion.identity);
            Player = (Player)PlayerCure.GetComponent(typeof(Player));
            CameraFollowScript.SetUpCamera(PlayerCure);
        }
    }
    public void RespawnPlayerInWorld()
    {
        if (GameManager.GetPlayerSelected() == DISEASE)
        {
            DiseasePlayerSelected = true;
            Vector3 PositionToSpawn = PlayerSpawner.GetRandomSpawnPoint();
            PositionToSpawn.y += 3;
            GameObject PlayerDisease = (GameObject)Instantiate(Disease, PositionToSpawn, Quaternion.identity);
            Player = (Player)PlayerDisease.GetComponent(typeof(Player));
            CameraFollowScript.SetUpCamera(PlayerDisease);

        }
        else if (GameManager.GetPlayerSelected() == CURE)
        {
            CurePlayerSelected = true;
            Vector3 PositionToSpawn = PlayerSpawner.GetRandomSpawnPoint();
            PositionToSpawn.y += 3;
            GameObject PlayerCure = (GameObject)Instantiate(Cure, PositionToSpawn, Quaternion.identity);
            Player = (Player)PlayerCure.GetComponent(typeof(Player));
            CameraFollowScript.SetUpCamera(PlayerCure);
        }
    }
    public bool IsCurePlayerSelected()
    {
        return CurePlayerSelected;
    }
    public bool IsDiseasePlayerSelected()
    {
        return DiseasePlayerSelected;
    }
}
