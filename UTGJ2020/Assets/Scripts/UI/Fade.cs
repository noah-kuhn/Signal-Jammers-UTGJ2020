using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {
    [Tooltip("")]
    [Range(0,3)]public float fadeInSpeed = 1.0f;
    private Animator anim;
    
    private void Awake() {
        anim = GetComponent<Animator>();
        anim.speed = fadeInSpeed;
    }
    // Update is called once per frame
    void Update() {

    }

    public void FadeOut(float speed) {
        anim.speed = speed;
        anim.SetBool("Fade", true);
    }


}


