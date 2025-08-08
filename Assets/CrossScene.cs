using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public static class CrossScene
{
    private static int gridSize = -1;

    // 0 = easy
    // 1 = normal
    // 2 = hard
    private static int difficulty = -1;

    private static float soundFXVolume = 0.25f;

    public static void SetGridSize(int size)
    {
        gridSize = size;
    }

    public static int GetGridSize()
    {
        return gridSize;
    }
    
    public static void SetDifficulty(int diff)
    {
        difficulty = diff;
    }

    public static int GetDifficulty()
    {
        return difficulty;
    }

    public static void SetSoundFXVolume(float volume)
    {
        soundFXVolume = volume;
    }

    public static float GetSoundFXVolume()
    {
        return soundFXVolume;
    }


}
