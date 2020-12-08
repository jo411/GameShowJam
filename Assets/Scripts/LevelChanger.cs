using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    private string levelToLoad;

    public static bool levelLoaded = false;

    public void FadeToLevel(string level)
    {
        levelToLoad = level;
        animator.SetTrigger("FadeOut");
        levelLoaded = false;
    }

    public void FadeToSameLevel()
    {
        levelToLoad = SceneManager.GetActiveScene().name;
        animator.SetTrigger("FadeOut");
        levelLoaded = false;
    }

    public void OnFadeOutComplete()
    {
        SceneManager.LoadSceneAsync(levelToLoad);
        animator.SetTrigger("FadeIn");
    }

    public void OnFadeInComplete()
    {
        levelLoaded = true;
    }
}
