using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    Resolution[] resolutions;

    public TMP_Dropdown resolutionDropdown;

    public Slider brightnessSlider;
    public Image blackoutImage;
    public TextMeshProUGUI brightnessText;

    public float minBrightness = 0.1f;

    public GameObject mainSettings;
    public GameObject keyboardMenu;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;

        if(Screen.fullScreen)
            Debug.Log("FullScreen On");
        else
            Debug.Log("FullScreen Off");
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetBrightness(float brightnessValue)
    {

        brightnessValue = Mathf.Clamp01(brightnessValue);

        if (brightnessValue < minBrightness)
        {
            brightnessValue = minBrightness;
        }

        Color color = blackoutImage.color;
        color.a = 1 - brightnessValue;
        blackoutImage.color = color;

        brightnessText.text = brightnessValue.ToString("0.00");
    }

    public void ShowKeyboardMenu()
    {
        mainSettings.SetActive(false);
        keyboardMenu.SetActive(true);
    }

    public void ShowMainSettings()
    {
        mainSettings.SetActive(true);
        keyboardMenu.SetActive(false);
    }

}
