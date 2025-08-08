using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

    public TextMeshProUGUI gridText;
    public Toggle easyToggle;
    public Toggle normalToggle;
    public Toggle hardToggle;
    public Slider gridSlider;
    public AudioSource cameraAudioSource;

    private void Start()
    {
        cameraAudioSource.volume = CrossScene.GetSoundFXVolume();
    }

    public void OnGridSizeSliderUpdate(float value)
    {
        CrossScene.SetGridSize((int)value);
        gridText.text = "Grid Size: " + (int)value + "x" + (int)value;
    }

    public void PlayGame()
    {
        if(easyToggle.isOn) CrossScene.SetDifficulty(0);
        else if(normalToggle.isOn) CrossScene.SetDifficulty(1);
        else if(hardToggle.isOn) CrossScene.SetDifficulty(2);

        CrossScene.SetGridSize((int)gridSlider.value);
        
        SceneManager.LoadScene("GameScene");
    }

    public void EasySelected()
    {
        normalToggle.SetIsOnWithoutNotify(false);
        hardToggle.SetIsOnWithoutNotify(false);
        easyToggle.SetIsOnWithoutNotify(true);
        CrossScene.SetDifficulty(0);
    }
    
    
    public void NormalSelected()
    {
        normalToggle.SetIsOnWithoutNotify(true);
        hardToggle.SetIsOnWithoutNotify(false);
        easyToggle.SetIsOnWithoutNotify(false);
        CrossScene.SetDifficulty(1);
    }
    
    public void HardSelected()
    {
        normalToggle.SetIsOnWithoutNotify(false);
        hardToggle.SetIsOnWithoutNotify(true);
        easyToggle.SetIsOnWithoutNotify(false);
        CrossScene.SetDifficulty(2);
    }
    
    
}
