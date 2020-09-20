using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag == "Player"){
            PlayerManager.SaveCurrentInfoAsLSD();
        }
    }
}
