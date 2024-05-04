using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
   

    public TMP_Dropdown resolutionDropdown;

 
    Resolution[] resolutions;
    void Start()
    {
        mainMenu.gameObject.SetActive(true); 
        optionsMenu.gameObject.SetActive(false);


        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume(); 
        }
        else
        {
            SetVolume();
        }


        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }



    /*Set Resolution */
    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    /* Main Volume Option Slider */
    public void SetVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    private void LoadVolume()
    {
        float playerMusic;
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        playerMusic = musicSlider.value;

        SetVolume();
    }

    /* Set Quality */
    public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /* Toggle Fullscreen */
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

}
