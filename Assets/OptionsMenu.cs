using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public Slider slider;
    public Music musicManager;
    public TextMeshProUGUI musicText;
    public TextMeshProUGUI effectsText;
    public Canvas pauseMenu;
    public AudioSource cameraAudioSource;

    private void Start()
    {
        if(musicManager == null) musicManager = GameObject.Find("Music").GetComponent<Music>();
        slider.value = musicManager.getVolume();
        cameraAudioSource.volume = CrossScene.GetSoundFXVolume();
    }

    public void Toggle()
    {
        if (pauseMenu != null)
        {
            pauseMenu.gameObject.SetActive(true);
        }
        gameObject.SetActive(false);
    }
    
    public void OnMusicSliderValueChanged(float value)
    {
        musicManager.ChangeVolume(value);
        musicText.text = "Music Volume: " + Math.Round(value * 100) + "%";
    }

    public void OnEffectsSliderValueChanged(float value)
    {
        effectsText.text = "Effects Volume: " + Math.Round(value * 100) + "%";
        CrossScene.SetSoundFXVolume(value);
        if (cameraAudioSource != null)
        {
            cameraAudioSource.volume = value;
            cameraAudioSource.Play();
        }
    }
}
