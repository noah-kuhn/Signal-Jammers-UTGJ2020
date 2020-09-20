using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 spawnPosition;
    
    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag == "Death"){
            LevelManager.Instance.RestartScene();
        }
    }
}
