using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {
    [Tooltip("")]
    [Range(0,3)]public float fadeInSpeed = 1.0f;
    public Animator anim;
    public static Fade Instance { get; private set; }
    
    private void Awake() {
        anim.speed = fadeInSpeed;
        if (Instance == null)
        {
            Instance = this; //there is no UIManager-- so this can be it
            DontDestroyOnLoad(gameObject); //pls don't destroy thanks
        } else {
            Destroy(gameObject); //ok there's already a UIManager. so die
        }
        
    }
    // Update is called once per frame

    public void FadeOut(float speed) {
        anim.speed = speed;
        anim.SetBool("Fade", true);
    }


}


