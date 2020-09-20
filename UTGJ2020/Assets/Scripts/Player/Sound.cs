using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    // Start is called before the first frame update
    public AudioClip Clip;
    [Range(0f,1f)]
    public float Volume;
    public bool Loop;
    public AudioManager.SoundIDs Id;
    [HideInInspector]
    public AudioSource Source;
}
