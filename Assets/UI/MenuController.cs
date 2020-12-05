using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject mainCamera;
    public GameObject mainMenuLocation;
    public GameObject creditsMenuLocation;


    public void ButtonStart()
    {
        SceneManager.LoadScene(1);
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
}


