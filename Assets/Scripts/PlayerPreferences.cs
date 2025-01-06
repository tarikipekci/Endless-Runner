using UnityEngine;

public class PlayerPreferences : MonoBehaviour
{
    public static float generalSoundLevel;
    public static float musicSoundLevel;
    public static float ambientSoundLevel;
    public static int numberOfCoins;
    public static int selectedCharacter;
    public static float bestTime;
    
    public static void SetGeneralSound(float general)
    {
        PlayerPrefs.SetFloat("generalSoundLevel", general);
    }

    public static float GetGeneralSound()
    {
        return PlayerPrefs.GetFloat("generalSoundLevel");
    }

    public static void SetMusicSound(float music)
    {
        PlayerPrefs.SetFloat("musicSoundLevel", music);
    }

    public static float GetMusicSound()
    {
        return PlayerPrefs.GetFloat("musicSoundLevel");
    }

    public static void SetAmbientSound(float ambient)
    {
        PlayerPrefs.SetFloat("ambientSoundLevel", ambient);
    }

    public static float GetAmbientSound()
    {
        return PlayerPrefs.GetFloat("ambientSoundLevel");
    }
    
    public static void SetNumberOfCoins(int amount)
    {
        PlayerPrefs.SetInt("numberOfCoins", amount);
    }

    public static int GetNumberOfCoins()
    {
        return PlayerPrefs.GetInt("numberOfCoins");
    }
    
    public static void SetSelectedCharacter(int character)
    {
        PlayerPrefs.SetInt("selectedCharacter", character);
    }

    public static int GetSelectedCharacter()
    {
        return PlayerPrefs.GetInt("selectedCharacter");
    }
    
    public static void SetBestTime(float newBestTime)
    {
        PlayerPrefs.SetFloat("bestTime", newBestTime);
    }

    public static float GetBestTime()
    {
        return PlayerPrefs.GetFloat("bestTime");
    }

}
