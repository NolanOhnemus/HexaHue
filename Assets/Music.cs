using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    private AudioSource source;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        source = GetComponent<AudioSource>();
        if (source.volume <= 0.0001f)
        {
            StopMusic();
        }
    }

    public void PlayMusic()
    {
        if (source.isPlaying) return;
        source.Play();
    }

    public void StopMusic()
    {
        source.Stop();
    }

    public void ChangeVolume(float volume)
    {
        if (!source.isPlaying && volume > 0.0001f)
        {
            PlayMusic();
        }
        source.volume = volume;
        if (volume <= 0.0001f)
        {
            StopMusic();
        }
    }

    public float getVolume()
    {
        return source.volume;
    }
}
