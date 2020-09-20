using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : Platform
{
    new void Awake(){
        base.Awake();
        base.platformColor = ColorIDs.Colors.Blue;
    }
    
    new void OnTriggerEnter(Collider c){
        base.OnTriggerEnter(c);
        //if the puddle is active and our player is on (or *in*) the puddle
        if(c.CompareTag("Player") && this.active){
            StartCoroutine(SinkThroughTimer());
        }
    }

    void OnTriggerExit(Collider c){
        if(c.CompareTag("Player")
        ){
            StopCoroutine(SinkThroughTimer());
            var meshColor = gameObject.GetComponent<MeshRenderer>().material.color;
            meshColor.a = 1.0f;
            gameObject.GetComponent<MeshCollider>().enabled = true;
        }
    }

    IEnumerator SinkThroughTimer(){
        float duration = 3f;
        float timeElapsed = 0;
        while(timeElapsed <= duration){
            timeElapsed += Time.deltaTime;
            var meshColor = gameObject.GetComponent<MeshRenderer>().material.color;
            meshColor.a -= timeElapsed/2.99f;
            yield return null;
        }
        gameObject.GetComponent<MeshCollider>().enabled = false;
    }
}
