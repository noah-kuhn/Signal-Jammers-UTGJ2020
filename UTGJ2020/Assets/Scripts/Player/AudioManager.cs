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
    private bool fading;
    

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
                musicSource.Play();
                break;
            }
        }
    }
    
    public void PlaySound(SoundIDs id)
    {
        Sound sound = FindS(id);
        if (sound.fading)
        {
            StopCoroutine(DoFade(sound));
            sound.Source.Stop();
            sound.Source.volume = sound.Volume;
        }
        foreach (var s in sounds)
        {
            if (s.Id == id)
            {
                s.Source.Play();
                break;
            }
        }
    }

    public void StopSound(SoundIDs id)
    {
        FindSound(id).Stop();
    }

    public void FadeSound(SoundIDs id)
    {
        Sound s = FindS(id);
        if(s.Source.isPlaying && !s.fading) StartCoroutine(DoFade(s));
    }

    private IEnumerator DoFade(Sound s)
    {
        s.fading = true;
        while (s.Source.volume > .01)
        {
            s.Source.volume -= .02f;
            yield return null;
        }
        s.Source.Stop();
        s.Source.volume = s.Volume;
        s.fading = false;
    }

    private Sound FindS(SoundIDs id)
    {
        foreach (var s in sounds)
        {
            if (s.Id == id)
            {
                return s;
            }
        }
        return null;
    }

    private AudioSource FindSound(SoundIDs id)
    {
        foreach (var s in sounds)
        {
            if (s.Id == id)
            {
                return s.Source;
            }
        }
        return null;
    }
    
    
}
