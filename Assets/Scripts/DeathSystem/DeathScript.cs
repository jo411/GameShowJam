using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    public GameObject worldSpawn;
    private GameObject lastCheckPoint;
    public GameObject MainCamera;
    private CameraLook cameraLook;
    GameObject LevelChangerObj;

    private void Start()
    {
        cameraLook = MainCamera.GetComponent<CameraLook>() as CameraLook;
        LevelChangerObj = GameObject.Find("LevelChanger") as GameObject;

        if (worldSpawn == null)
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
        if (LevelChangerObj != null)
        {
            LevelChanger levelChanger = LevelChangerObj.GetComponent(typeof(LevelChanger)) as LevelChanger;
            levelChanger.FadeToSameLevel();
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void GoToLocation(GameObject location)
    {
        this.transform.position = location.transform.position;
        this.transform.rotation = location.transform.rotation;
        cameraLook.ResetCamera();
        Input.ResetInputAxes();
    }

    public void SetCheckpoint(GameObject location)
    {
        lastCheckPoint = location;
    }
}
