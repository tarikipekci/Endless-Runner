using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public static bool isGameStarted;
    public static Mesh selectedCharacterMesh;
    public SkinnedMeshRenderer currentCharacterMesh;

    public static int numberOfCoins;
    public Text numberOfCoinsText;
    public GameObject startingText;

    private void Awake()
    {
        currentCharacterMesh.sharedMesh = selectedCharacterMesh;
    }

    private void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        Application.targetFrameRate = 120;
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

    public static void SaveCoins()
    {
        var currentCoinAmount = PlayerPreferences.GetNumberOfCoins();
        var newValue = currentCoinAmount + numberOfCoins;
        PlayerPreferences.SetNumberOfCoins(newValue);
    }

    public void GoBackToMainMenu()
    {
        SaveCoins();
        SceneManager.LoadScene("Menu");
    }
}
