using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SoundIDs
    {
        MAIN_MUSIC,
        BURST,
        WALK,
        LAND
        
    }

    public float masterVolume;
    public Sound[] sounds;
    public AudioSource musicSource;

    

    private void Awake()
    {
        foreach (var s in sounds)
        {
            if (s.Id != SoundIDs.MAIN_MUSIC)
            {
                s.Source = gameObject.AddComponent<AudioSource>();
                s.Source.clip = s.Clip;
                s.Source.volume = s.Volume;
                s.Source.loop = s.Loop;
            }
        }
        musicSource.loop = true;
    }

    public void PlayMusic(SoundIDs id)
    {
        foreach (var s in sounds)
        {
            if (s.Id == id)
            {
                musicSource.clip = s.Clip;
                musicSource.volume = s.Volume;
            }
        }
    }
    
    public void PlaySound(SoundIDs id)
    {
        foreach (var s in sounds)
        {
            if (s.Id == id)
            {
                s.Source.Play();
                break;
            }
        }
    }
    
}
