using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public static bool isGamePaused;
    public GameObject options;
    
    public void TryAgain()
    {
        SceneManager.LoadScene("Level");
        PlayerManager.SaveCoins();
        PlayerManager.SaveBestTime();
    }

    public void PauseGame()
    {
        if (isGamePaused)
        {
            isGamePaused = false;
            Time.timeScale = 1;
            options.SetActive(false);
        }
        else
        {
            isGamePaused = true;
            Time.timeScale = 0;
            options.SetActive(true);
        }
    }
}
