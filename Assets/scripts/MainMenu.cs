using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string level;

    public void PlayGame()
    {
        // reset deathcount
        PlayerPrefs.SetInt("DeathCount", 0);
        SceneManager.LoadSceneAsync(1);
    }

    public void ContinueGame()
    {
        // get previous level, if none initialize to first level
        level = PlayerPrefs.GetString("Level", "Tomato-1");
        SceneManager.LoadSceneAsync(level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Home()
    {
        // go to title screen
        SceneManager.LoadSceneAsync("title");
    }
}
