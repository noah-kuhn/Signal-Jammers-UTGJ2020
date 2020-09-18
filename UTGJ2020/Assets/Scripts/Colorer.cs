using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorer : MonoBehaviour
{
    public ParticleSystem partSys;
    public bool canBurst;

    void Start(){
        partSys = GetComponent<ParticleSystem>();
        canBurst = false;
    }

    void Update(){
        canBurst = !partSys.IsAlive();
    }
}
