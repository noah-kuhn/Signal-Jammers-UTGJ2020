using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag == "Death"){
            LevelManager.Instance.RestartScene();
        }
    }
}
