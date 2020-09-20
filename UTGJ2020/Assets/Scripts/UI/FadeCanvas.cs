using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class FadeCanvas: MonoBehaviour
{
    // Start is called before the first frame update
    public static FadeCanvas Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; //there is no UIManager-- so this can be it
            DontDestroyOnLoad(gameObject); //pls don't destroy thanks
        } else {
            Destroy(gameObject); //ok there's already a UIManager. so die
        }
    }
}
