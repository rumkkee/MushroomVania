using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer mainMixer;
    public AudioMixer musicMixer;
    public AudioMixer ambientMixer;
    public Slider mainVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider ambientVolumeSlider;
    public TextMeshProUGUI mainVolumeText;
    public TextMeshProUGUI musicVolumeText;
    public TextMeshProUGUI ambientVolumeText;

    private const string MAIN_VOLUME_PARAM = "MainVolume";
    private const string MUSIC_VOLUME_PARAM = "MusicVolume";
    private const string AMBIENT_VOLUME_PARAM = "AmbientVolume";

    // Initializes volume sliders and sets their values from player preferences
    private void Start()
    {
        mainVolumeSlider.onValueChanged.AddListener(SetMainVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        ambientVolumeSlider.onValueChanged.AddListener(SetAmbientVolume);

        mainVolumeSlider.value = PlayerPrefs.GetFloat(MAIN_VOLUME_PARAM, 1f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME_PARAM, 1f);
        ambientVolumeSlider.value = PlayerPrefs.GetFloat(AMBIENT_VOLUME_PARAM, 1f);
    }

    // Updates volume display texts
    private void UpdateVolumeTexts()
    {
        mainVolumeText.text = mainVolumeSlider.value.ToString("0.00");
        musicVolumeText.text = musicVolumeSlider.value.ToString("0.00");
        ambientVolumeText.text = ambientVolumeSlider.value.ToString("0.00");
    }

    // Sets the main volume level and updates the corresponding player preference
    public void SetMainVolume(float volume)
    {
        mainMixer.SetFloat(MAIN_VOLUME_PARAM, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MAIN_VOLUME_PARAM, volume);
        PlayerPrefs.Save();
        UpdateVolumeTexts();
    }

    // Sets the music volume level and updates the corresponding player preference
    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat(MUSIC_VOLUME_PARAM, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_PARAM, volume);
        PlayerPrefs.Save();
        UpdateVolumeTexts();
    }

    // Sets the ambient volume level and updates the corresponding player preference
    public void SetAmbientVolume(float volume)
    {
        ambientMixer.SetFloat(AMBIENT_VOLUME_PARAM, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(AMBIENT_VOLUME_PARAM, volume);
        PlayerPrefs.Save();
        UpdateVolumeTexts();
    }

    // Resets volume sliders and volume levels to default values
    public void ResetVolume()
    {
        mainVolumeSlider.value = 1f;
        musicVolumeSlider.value = 1f;
        ambientVolumeSlider.value = 1f;

        SetMainVolume(1f);
        SetMusicVolume(1f);
        SetAmbientVolume(1f);
    }
}
