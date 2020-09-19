using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {
    [Tooltip("")]
    public int fadeInSpeed = 1;
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


