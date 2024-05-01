using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // Array to store available screen resolutions
    Resolution[] resolutions;

    // Dropdown UI element for resolution selection
    public TMP_Dropdown resolutionDropdown;

    // UI elements for brightness adjustment
    public Slider brightnessSlider;
    public Image blackoutImage;
    public TextMeshProUGUI brightnessText;

    // Minimum brightness value allowed
    public float minBrightness = 0.1f;

    // UI elements for menu navigation
    public GameObject mainSettings;
    public GameObject keyboardMenu;

    void Start()
    {
        // Retrieve available screen resolutions
        resolutions = Screen.resolutions;

        // Clear resolution dropdown options
        resolutionDropdown.ClearOptions();

        // List to store resolution options
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        // Loop through available resolutions
        for(int i = 0; i < resolutions.Length; i++){
            // Construct resolution option string
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Check if current resolution matches, set index accordingly
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        // Add options to resolution dropdown
        resolutionDropdown.AddOptions(options);
        // Set current resolution in dropdown
        resolutionDropdown.value = currentResolutionIndex;
        // Refresh dropdown display
        resolutionDropdown.RefreshShownValue();
    }

    // Set the selected resolution
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Toggle between fullscreen and windowed mode
    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;

        // Log whether fullscreen mode is enabled or not
        if(Screen.fullScreen)
            Debug.Log("FullScreen On");
        else
            Debug.Log("FullScreen Off");
    }

    // Set the graphics quality level
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Set the screen brightness
    public void SetBrightness(float brightnessValue)
    {
        // Clamp brightness value between 0 and 1
        brightnessValue = Mathf.Clamp01(brightnessValue);

        // Ensure minimum brightness is respected
        if (brightnessValue < minBrightness)
        {
            brightnessValue = minBrightness;
        }

        // Adjust black background color to modify brightness
        Color color = blackoutImage.color;
        color.a = 1 - brightnessValue;
        blackoutImage.color = color;

        // Update displayed brightness value
        brightnessText.text = brightnessValue.ToString("0.00");
    }

    // Show the keyboard configuration menu
    public void ShowKeyboardMenu()
    {
        mainSettings.SetActive(false);
        keyboardMenu.SetActive(true);
    }

    // Show the main settings menu
    public void ShowMainSettings()
    {
        mainSettings.SetActive(true);
        keyboardMenu.SetActive(false);
    }
}
