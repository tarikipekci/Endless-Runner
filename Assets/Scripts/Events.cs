using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public void TryAgain()
    {
        SceneManager.LoadScene("Level");
        PlayerManager.SaveCoins();
    }
}
