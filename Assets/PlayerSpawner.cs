using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour 
{
    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;
    public GameObject SpawnPoint3;
    public GameObject SpawnPoint4;

    List<GameObject> PlayerSpawnPoints = new List<GameObject>();
	void Start () 
    {
        PlayerSpawnPoints.Add(SpawnPoint1);
        PlayerSpawnPoints.Add(SpawnPoint2);
        PlayerSpawnPoints.Add(SpawnPoint3);
        PlayerSpawnPoints.Add(SpawnPoint4);
    }
	void Update () 
    {
	
	}
    public Vector3 GetRandomSpawnPoint()
    {
        Vector3 SpawnPointPosition = Vector3.zero;
        int RandomIndexInList = Random.Range(0, 3); // inclusive both
        PlayerSpawnPoint SpawnPointDetail = (PlayerSpawnPoint)PlayerSpawnPoints[RandomIndexInList].GetComponent<PlayerSpawnPoint>();
        if(!SpawnPointDetail.IsSpawnPointUtilized())
        {
            SpawnPointPosition = PlayerSpawnPoints[RandomIndexInList].transform.position;
        }
        else
        {
            GetRandomSpawnPoint();
        }
        return SpawnPointPosition;
    }
}
