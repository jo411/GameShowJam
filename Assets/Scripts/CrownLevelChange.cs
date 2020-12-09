using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrownLevelChange : MonoBehaviour
{

    GameObject LevelChangerObj;
    // Start is called before the first frame update
    void Start()
    {
        LevelChangerObj = GameObject.Find("LevelChanger") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
        {
            return;
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (LevelChangerObj != null)
        {
            LevelChanger levelChanger = LevelChangerObj.GetComponent(typeof(LevelChanger)) as LevelChanger;
            levelChanger.FadeToLevel("MainMenu");
        }
        else
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }


    }

}
