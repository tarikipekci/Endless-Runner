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
    public static AudioManager audioManager;

    public static int numberOfCoins;
    public Text numberOfCoinsText;
    public GameObject startingText;
    public Text counterText;
    private static float elapsedTime;

    private void Awake()
    {
        currentCharacterMesh.sharedMesh = selectedCharacterMesh;
    }

    private void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        numberOfCoins = 0;
        isGameStarted = false;
        elapsedTime = 0f;
        audioManager = FindObjectOfType<AudioManager>();
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
        
        elapsedTime += Time.deltaTime; 

        int hours = Mathf.FloorToInt(elapsedTime / 3600);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        counterText.text = $"{hours:00}:{minutes:00}:{seconds:00}";
    }

    public static void SaveCoins()
    {
        var currentCoinAmount = PlayerPreferences.GetNumberOfCoins();
        var newValue = currentCoinAmount + numberOfCoins;
        PlayerPreferences.SetNumberOfCoins(newValue);
    }

    public static void SaveBestTime()
    {
        var currentBestTime = PlayerPreferences.GetBestTime();
        var newTime = elapsedTime;

        if (newTime > currentBestTime)
        {
            PlayerPreferences.SetBestTime(newTime);
        }
    }
    
    public void GoBackToMainMenu()
    {
        SaveCoins();
        SaveBestTime();
        SceneManager.LoadScene("Menu");
        Events.isGamePaused = false;
    }
}
