using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace PauseMenu
{
    public class Settings : MonoBehaviour
    {
        public AudioMixer audioMixer;
        public Dropdown resolutionDropdown;
        private float currentVolume;
        private Resolution[] resolutions;

        void Start()
        {
            resolutionDropdown.ClearOptions();
            var options = new List<string>();
            resolutions = Screen.resolutions;
            var currentResolutionIndex = 0;

            for (var i = 0; i < resolutions.Length; i++)
            {
                var option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
                options.Add(option);
                if (resolutions[i].width == Screen.currentResolution.width 
                    && resolutions[i].height == Screen.currentResolution.height)
                    currentResolutionIndex = i;
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.RefreshShownValue();
            LoadSettings(currentResolutionIndex);
        }

        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("Volume", volume);
            currentVolume = volume;
        }
    
        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        public void SetResolution(int resolutionIndex)
        {
            var resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    
        public void SaveSettings()
        {
            PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
            PlayerPrefs.SetInt("FullscreenPreference", System.Convert.ToInt32(Screen.fullScreen));
            PlayerPrefs.SetFloat("VolumePreference", currentVolume);
        }

        private void LoadSettings(int currentResolutionIndex)
        {
            resolutionDropdown.value = PlayerPrefs.HasKey("ResolutionPreference")
                ? PlayerPrefs.GetInt("ResolutionPreference")
                : currentResolutionIndex;
            Screen.fullScreen = !PlayerPrefs.HasKey("FullscreenPreference")
                                || System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        }
    }
}
