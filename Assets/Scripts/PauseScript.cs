using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{

    public GameObject PauseMenu;
    private static bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (LevelChanger.levelLoaded)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                Scene scene = SceneManager.GetActiveScene();

                if (scene.name != "MainMenu")
                {
                    if (!isPaused)
                    {
                        activateMenu();
                    }
                    else
                    {
                        deactivateMenu();
                    }
                }
            }
        }
    }

    void activateMenu()
    {
        isPaused = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PauseMenu.SetActive(true);
    }

    public void deactivateMenu()
    {
        isPaused = false;
        AudioListener.pause = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        deactivateMenu();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        GameObject LevelChangerObj = GameObject.Find("LevelChanger") as GameObject;

        if (LevelChangerObj != null)
        {
            LevelChanger levelChanger = LevelChangerObj.GetComponent(typeof(LevelChanger)) as LevelChanger;
            levelChanger.FadeToLevel("MainMenu");
        } else
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }

    }
}
