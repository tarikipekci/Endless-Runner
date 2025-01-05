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
    public GameObject shop;
    private bool isShopActive;

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
}
