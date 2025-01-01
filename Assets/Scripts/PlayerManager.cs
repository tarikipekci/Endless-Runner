using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static int numberOfCoins;
    public Text numberOfCoinsText;

    private void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        Application.targetFrameRate = 90;
        numberOfCoins = 0;
    }

    private void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        
        numberOfCoinsText.text = "Coins: " + numberOfCoins;
    }
}
