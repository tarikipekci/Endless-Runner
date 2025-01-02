using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public static bool isGameStarted;

    public static int numberOfCoins;
    public Text numberOfCoinsText;
    public GameObject startingText;

    private void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        Application.targetFrameRate = 90;
        numberOfCoins = 0;
        isGameStarted = false;
    }

    private void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(startingText);
        }
        
        numberOfCoinsText.text = "Coins: " + numberOfCoins;
    }
}
