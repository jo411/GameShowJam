using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    public GameObject worldSpawn;
    private GameObject lastCheckPoint;
    public GameObject MainCamera;

    private void Start()
    {
        if(worldSpawn == null)
        {
            GameObject notsetSpawnObject = new GameObject();
            notsetSpawnObject.transform.position = this.gameObject.transform.position;
            notsetSpawnObject.transform.rotation = this.gameObject.transform.rotation;
            notsetSpawnObject.name = "AutoGenWorldSpawnPoint";
            worldSpawn = notsetSpawnObject;
        }
    }

    public void ResetPlayer()
    {
        if (lastCheckPoint != null)
        {
            GoToLocation(lastCheckPoint);
        } else {
            GoToLocation(worldSpawn);
        }
    }

    public void KillPlayer()
    {
        GoToLocation(worldSpawn);
    }

    void GoToLocation(GameObject location)
    {
        this.transform.position = location.transform.position;
        this.transform.rotation = location.transform.rotation;
        MainCamera.transform.rotation = location.transform.rotation;
    }

    public void SetCheckpoint(GameObject location)
    {
        lastCheckPoint = location;
    }
}
