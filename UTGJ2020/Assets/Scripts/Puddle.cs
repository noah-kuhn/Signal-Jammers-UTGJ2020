using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : Platform
{
    bool timerRunning;
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
        if(c.gameObject.tag == "Player"){
            timerRunning = false;
            StopCoroutine(SinkThroughTimer());
            _renderer.material = active ? onMaterial : offMaterial;
            //meshColor.a = 1.0f;
            gameObject.GetComponent<MeshCollider>().enabled = true;
        }
    }

    IEnumerator SinkThroughTimer(){
        timerRunning = true;
        float duration = 3f;
        float timeElapsed = 0;
        while(timeElapsed <= duration && timerRunning){
            timeElapsed += Time.deltaTime;
            if(timeElapsed > 1.5 && timeElapsed < 1.6 ||
                timeElapsed > 1.7 && timeElapsed < 1.8 ||
                timeElapsed > 1.9 && timeElapsed < 2 ||
                timeElapsed > 2.1 && timeElapsed < 2.15 ||
                timeElapsed > 2.2 && timeElapsed < 2.25 ||
                timeElapsed > 2.3 && timeElapsed < 2.35 ||
                timeElapsed > 2.4 && timeElapsed < 2.45 ||
                timeElapsed > 2.5 && timeElapsed < 2.55 ||
                timeElapsed > 2.6 && timeElapsed < 2.65 ||
                timeElapsed > 2.7 && timeElapsed < 2.75 ||
                timeElapsed > 2.8 && timeElapsed < 2.85 ||
                timeElapsed > 2.9 && timeElapsed < 2.95){
                _renderer.material = offMaterial;
            }else{
                _renderer.material = onMaterial;
            }
            yield return null;
        }
        yield return new WaitUntil(() => timeElapsed >= duration);
        gameObject.GetComponent<MeshCollider>().enabled = false;
        this.active = false;
    }
}
