using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider generalSoundSlider, ambientSoundSlider, musicSoundSlider;
    [SerializeField] private Text generalSoundLevel, ambientSoundLevel, musicSoundLevel;
    [SerializeField] private GameObject settings;
    private AudioManager audioManager;
    private bool isSettingsOpen;
    private const float hundred = 100f;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("generalSoundLevel"))
        {
            PlayerPreferences.SetGeneralSound(0.5f);
        }

        if (!PlayerPrefs.HasKey("ambientSoundLevel"))
        {
            PlayerPreferences.SetAmbientSound(0.5f);
        }
        
        if (!PlayerPrefs.HasKey("musicSoundLevel"))
        {
            PlayerPreferences.SetMusicSound(0.5f);
        }
        
        Application.targetFrameRate = 144;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        generalSoundSlider.value = PlayerPreferences.GetGeneralSound();
        ambientSoundSlider.value = PlayerPreferences.GetAmbientSound();
        musicSoundSlider.value = PlayerPreferences.GetMusicSound();
        GeneralSoundSlider(PlayerPreferences.GetGeneralSound());
        AmbientSoundSlider(PlayerPreferences.GetAmbientSound());
        MusicSoundSlider(PlayerPreferences.GetMusicSound());
        gameObject.SetActive(false);
    }
    
    public void GeneralSoundSlider(float volume)
    {
        AudioListener.volume = generalSoundSlider.value;
        generalSoundSlider.value = AudioListener.volume;
        generalSoundLevel.text = Mathf.FloorToInt(AudioListener.volume * hundred).ToString();
        PlayerPreferences.SetGeneralSound(AudioListener.volume);
    }

    public void AmbientSoundSlider(float volume)
    {
        volume = ambientSoundSlider.value;
        ambientSoundLevel.text = Mathf.FloorToInt(volume * hundred).ToString();
        audioManager.SetAmbientSoundLevel(volume);
        PlayerPreferences.SetAmbientSound(volume);
    }

    public void MusicSoundSlider(float volume)
    {
        volume = musicSoundSlider.value;
        musicSoundLevel.text = Mathf.FloorToInt(volume * hundred).ToString();
        audioManager.SetMusicSoundLevel(volume);
        PlayerPreferences.SetMusicSound(volume);
    }
}