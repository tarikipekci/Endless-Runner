using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text[] texts;
    public Image[] images;
    public CharacterInfo[] characters;
    public Text numberOfCoins;
    public GameObject shop;
    private bool isShopActive;
    
    public void PurchaseCharacter(CharacterInfo character)
    {
        var newValue = PlayerPreferences.GetNumberOfCoins() - character.characterPrice;
        PlayerPreferences.SetNumberOfCoins(newValue);
        LoadCoins();
    }

    private void SetShopProductInfos()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            texts[i].text = characters[i].characterPrice + " Coin";
            images[i].sprite = characters[i].characterIcon;
        }
    }

    private void Start()
    {
        SetShopProductInfos();
    }
    
    private void LoadCoins()
    {
        numberOfCoins.text = "Coins: " + PlayerPreferences.GetNumberOfCoins();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenCloseShop()
    {
        if (isShopActive)
        {
            shop.SetActive(false);
            isShopActive = false;
        }
        else
        {
            shop.SetActive(true);
            isShopActive = true;
            LoadCoins();
        }
    }
}
