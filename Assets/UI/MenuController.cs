using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject mainCamera;
    public GameObject mainMenuLocation;
    public GameObject creditsMenuLocation;
    public GameObject levelMenuLocation;

    [Scene]
    public List<string> levels;

    public void Awake()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
    }

    public void ButtonStart()
    {
        
        string scene = Extensions.GetRandomElement(levels);
        goToLevel(scene);
    }

    public void ButtonLevels()
    {
        CameraMovmentController cameraMovment = mainCamera.GetComponent<CameraMovmentController>();
        cameraMovment.setCameraTarget(levelMenuLocation);
    }

    public void ButtonCredits()
    {
        CameraMovmentController cameraMovment = mainCamera.GetComponent<CameraMovmentController>();
        cameraMovment.setCameraTarget(creditsMenuLocation);
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }

    public void ButtonBack()
    {
        CameraMovmentController cameraMovment = mainCamera.GetComponent<CameraMovmentController>();
        cameraMovment.setCameraTarget(mainMenuLocation);
    }

    public void SpotifyLink()
    {
        Application.OpenURL("https://open.spotify.com/artist/2x4nKS8WqFNdbDg51xUvw1?si=6CAJwkOzT1-qsoSDE9F2MA");
    }

    private void goToLevel(string level)
    {
        SceneManager.LoadSceneAsync(level);
    }
}


