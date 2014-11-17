using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class HUDManager : MonoBehaviour 
{
    public Text ManaText;
    public Text LivesText;
    public Text ObjectiveText;

    public Button PlayCure;
    public Button PlayDisease;

    public GameObject Cure;
    public GameObject Disease;

    private Player Player;

    private bool CurePlayerSelected = false;
    private bool DiseasePlayerSelected = false;

    public GameObject Camera;

    private CameraFollower CameraFollowScript;
    private PlayerMovement MovementScript;

    private PlayerSpawner PlayerSpawner;
    void Start()
    {
        PlayerSpawner = Camera.GetComponent<PlayerSpawner>();
        CameraFollowScript = Camera.GetComponent<CameraFollower>();
    }
    public void SetupBrainEnergyGauge()
    {
        //LivesText.text = "Health: " + Player.GetHealth();
        //ManaText.text = "Mana: " + Player.GetMana();
    }
    public bool IsCurePlayerSelected()
    {
        return CurePlayerSelected;
    }
    public bool IsDiseasePlayerSelected()
    {
        return DiseasePlayerSelected;
    }
    void Update()
    {
       
    }
    public void UpdateLives()
    {
        Debug.Log("Player.getLives():" + Player.GetLives());
        LivesText.text = "Lives: " + Player.GetLives();
    }
    public void UpdateMana()
    {
        ManaText.text = "Mana: " + Player.GetMana();
    }
    public void DrawDamageTakenEffect()
    {

    }
    public void DrawLifeReplinishedEffect()
    {

    }
    public void DrawManaReplinishedEffect()
    {

    }
    public void OnScreenDebugLine(string DebugLine)
    {
        ObjectiveText.text ="DEBUG:" + DebugLine;
    }
    public void PlayerDiseaseClicked()
    {
        Debug.Log("PlayerDiseaseClicked");
        if (!CurePlayerSelected && !DiseasePlayerSelected)
        {
            DiseasePlayerSelected = true;

            PlayCure.gameObject.SetActive(false);
            PlayDisease.gameObject.SetActive(false);

            Vector3 PositionToSpawn = PlayerSpawner.GetRandomSpawnPoint();
            PositionToSpawn.y += 3;
            //GameObject PlayerDisease = (GameObject)Instantiate(Disease, new Vector3(-5.0f, 0.5f, 0f), Quaternion.identity);
            GameObject PlayerDisease = (GameObject)Instantiate(Disease, PositionToSpawn, Quaternion.identity);

            Player = (Player)PlayerDisease.GetComponent(typeof(Player));
            DoGeneralSetup(PlayerDisease);

            ObjectiveText.text = "Objective: You are a disease... \n1. Damage Synapses\n2. Destroy Dopamine Sacs \n3. Annihilate the Cure!";
        }
    }
    public void PlayerCureClicked()
    {
        Debug.Log("PlayerCureClicked");
        if (!DiseasePlayerSelected && !CurePlayerSelected)
        {
            CurePlayerSelected = true;

            PlayDisease.gameObject.SetActive(false);
            PlayCure.gameObject.SetActive(false);

            Vector3 PositionToSpawn = PlayerSpawner.GetRandomSpawnPoint();
            PositionToSpawn.y += 3;
            //GameObject PlayerCure = (GameObject)Instantiate(Cure, new Vector3(-5.0f, 0.5f, 0f), Quaternion.identity);
            GameObject PlayerCure = (GameObject)Instantiate(Cure, PositionToSpawn, Quaternion.identity);

            Player = (Player)PlayerCure.GetComponent(typeof(Player));
            DoGeneralSetup(PlayerCure);

            ObjectiveText.text = "Objective: You are a cure... \n1. Repair Synapses\n2. Release Dopamine Sacs \n3. Annihilate the Disease!";
        }
    }
    public void DoGeneralSetup(GameObject Player)
    {
        SetupBrainEnergyGauge();
        CameraFollowScript.SetUpCamera(Player);
    }
}
