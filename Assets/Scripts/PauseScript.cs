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
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
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

    void activateMenu()
    {
        isPaused = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;
        PauseMenu.SetActive(true);
        
    }

    public void deactivateMenu()
    {
        isPaused = false;
        PauseMenu.SetActive(false);
        AudioListener.pause = false;
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        LevelChanger levelChanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
        deactivateMenu();
        levelChanger.FadeToLevel("MainMenu");
    }
}
