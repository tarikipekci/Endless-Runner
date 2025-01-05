using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    private bool isGamePaused;
    
    public void TryAgain()
    {
        SceneManager.LoadScene("Level");
        PlayerManager.SaveCoins();
    }

    public void PauseGame()
    {
        if (isGamePaused)
        {
            isGamePaused = false;
            Time.timeScale = 1;
        }
        else
        {
            isGamePaused = true;
            Time.timeScale = 0;
        }
    }
}
