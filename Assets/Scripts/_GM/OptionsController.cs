using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OptionsController : MonoBehaviour {

    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;

    public Resolution[] resolutions;

    public PlayerPrefsManager playerPrefsManager;
    private MusicManager musicManager;

	// Use this for initialization
	void Start () {
        musicManager = GameObject.FindObjectOfType<MusicManager>();
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });

        resolutions = Screen.resolutions;
        foreach(Resolution resolution in resolutions){
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
	}
	
	// Update is called once per frame
	void Update () {
        musicManager.SetVolume(volumeSlider.value);
	}

    public void SaveAndExit(){
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);

        SceneManager.LoadScene("Menu");
    }

    public void SetDefault(){
        volumeSlider.value = 0.8f;
    }

    public void OnFullscreenToggle(){
        playerPrefsManager.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange(){
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
    }
}
