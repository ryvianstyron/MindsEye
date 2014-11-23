using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class HUDManager : MonoBehaviour
{
    const int DISEASE = 0;
    const int CURE = 1;

    public Text ManaText;
    public Text LivesText;
    public Text ObjectiveText;

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
        LivesText.text = "Lives: " + Player.GetLives();
    }
    void Start()
    {
        PlayerSpawner = Camera.GetComponent<PlayerSpawner>();
        CameraFollowScript = Camera.GetComponent<CameraFollower>();

        if (GameManager.GetPlayerSelected() == DISEASE)
        {
            Debug.Log("DISEASE PLAYER");

            DiseasePlayerSelected = true;
            Vector3 PositionToSpawn = PlayerSpawner.GetRandomSpawnPoint();
            PositionToSpawn.y += 3;
            GameObject PlayerDisease = (GameObject)Instantiate(Disease, PositionToSpawn, Quaternion.identity);
            Player = (Player)PlayerDisease.GetComponent(typeof(Player));
            CameraFollowScript.SetUpCamera(PlayerDisease);
            ObjectiveText.text = "Objective: You are a disease... \n1. Damage Synapses\n2. Destroy Dopamine Sacs \n3. Annihilate the Cure!";

        }
        else if (GameManager.GetPlayerSelected() == CURE)
        {
            Debug.Log("CURE PLAYER");
            CurePlayerSelected = true;
            Vector3 PositionToSpawn = PlayerSpawner.GetRandomSpawnPoint();
            PositionToSpawn.y += 3;
            GameObject PlayerCure = (GameObject)Instantiate(Cure, PositionToSpawn, Quaternion.identity);
            Player = (Player)PlayerCure.GetComponent(typeof(Player));
            CameraFollowScript.SetUpCamera(PlayerCure);
            ObjectiveText.text = "Objective: You are a cure... \n1. Repair Synapses\n2. Release Dopamine Sacs \n3. Annihilate the Disease!";
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
