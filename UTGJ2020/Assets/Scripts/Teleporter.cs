using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public string nextSceneName;
    
    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag == "Player"){
            LevelManager.Instance.ProgressToScene(nextSceneName);
        }
    }

}
