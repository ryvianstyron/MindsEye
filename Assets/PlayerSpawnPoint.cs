using UnityEngine;
using System.Collections;

public class PlayerSpawnPoint : MonoBehaviour 
{
    bool SpawnPointUtilized = false;

    public void SetSpawnPointUtilized()
    {
        SpawnPointUtilized = true;
    }
    public bool IsSpawnPointUtilized()
    {
        return SpawnPointUtilized;
    }
}
