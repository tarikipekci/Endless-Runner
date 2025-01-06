using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text[] texts;
    public Image[] images;
    public CharacterInfo[] characters;
    public Text numberOfCoins;
    public Text bestTimeText;
    public GameObject shop;
    public GameObject options;
    private bool isShopActive;
    private bool isOptionsActive;

    public void PurchaseCharacter(CharacterInfo character)
    {
        var thisCharacterIndex = Array.IndexOf(characters, character);
        if (!character.purchased)
        {
            var currentCoin = PlayerPreferences.GetNumberOfCoins();
            if (currentCoin >= character.characterPrice)
            {
                var newValue = currentCoin - character.characterPrice;
                PlayerPreferences.SetNumberOfCoins(newValue);
                LoadCoins();
                character.purchased = true;
                texts[thisCharacterIndex].text = "Select";
            }
        }
        else
        {
            texts[PlayerPreferences.GetSelectedCharacter()].text = "Select";
            PlayerPreferences.SetSelectedCharacter(thisCharacterIndex);
            texts[PlayerPreferences.GetSelectedCharacter()].text = "Selected";
        }
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
        characters[0].purchased = true;
        var bestTime = PlayerPreferences.GetBestTime();
        
        int hours = Mathf.FloorToInt(bestTime / 3600);
        int minutes = Mathf.FloorToInt((bestTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(bestTime % 60);

        bestTimeText.text = "Best Time: " + $"{hours:00}:{minutes:00}:{seconds:00}";
    }
    
    private void LoadCoins()
    {
        numberOfCoins.text = "Coins: " + PlayerPreferences.GetNumberOfCoins();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
        var selectedCharacter = PlayerPreferences.GetSelectedCharacter();
        var selectedCharacterMesh = characters[selectedCharacter].characterMesh;
        PlayerManager.selectedCharacterMesh = selectedCharacterMesh;
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
            var currentCharacter = PlayerPreferences.GetSelectedCharacter();
            texts[currentCharacter].text = "Selected";
            
            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i].purchased && i != currentCharacter)
                {
                    texts[i].text = "Select";
                }
            }
        }
    }

    public void OpenCloseOptions()
    {
        if (isOptionsActive)
        {
            options.SetActive(false);
            isOptionsActive = false;
        }
        else
        {
            options.SetActive(true);
            isOptionsActive = true;
        }
    }
}
