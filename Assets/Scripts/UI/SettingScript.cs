using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingScript : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;
    public Slider volumeSlider;
    public Toggle fullScreenToggle;

    public GameObject menuSetting, menuHowToPlay, menuExit;

    Resolution[] resolutions;
    float currentVolume;
    int currentResolitionIndex;

    private void Awake()
    {
        
    }
    void Start()
    {

        //Update full screen
        if (PlayerPrefs.GetInt("isFullScreen") == 1)
        {
            
            fullScreenToggle.isOn = true;
        }
        else
        {
            fullScreenToggle.isOn = false;
        }   
        resolutions = Screen.resolutions;
        resolutionDropdown.value = PlayerPrefs.GetInt("currentResolitionIndex");
        resolutionDropdown.RefreshShownValue();
    }

 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu(menuSetting);
            CloseMenu(menuHowToPlay);
            CloseMenu(menuExit);
        }

        //Update Audio Mixer
        audioMixer.GetFloat("volume", out currentVolume);
        PlayerPrefs.SetFloat("volume", currentVolume);
        volumeSlider.value = currentVolume;

        


    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("currentResolitionIndex", resolutionIndex);
    } 

    public void FullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        if (isFullScreen)
        {
            PlayerPrefs.SetInt("isFullScreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isFullScreen", 0);
        }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void CloseMenu (GameObject menuObject)
    {
        menuObject.SetActive(false);
    }
    
}

