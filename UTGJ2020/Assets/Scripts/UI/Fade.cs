﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {
    [Tooltip("")]
    [Range(0,3)]public float fadeInSpeed = 1.0f;
    public Animator anim;
    
    
    private void Awake() {
        anim = GetComponent<Animator>();
        anim.speed = fadeInSpeed;
    }

    public void FadeOut(float speed) {
        anim.speed = speed;
        anim.SetBool("Fade", true);
    }


}


