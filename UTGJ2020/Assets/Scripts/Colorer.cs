using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorer : MonoBehaviour
{
    public ParticleSystem partSys;
    public bool canBurst; //can we do the explosion thing?
    private SphereCollider _collider; //the actual trigger for the burst
    public float burstSize;
    
    void Start(){
        partSys = GetComponent<ParticleSystem>();
        _collider = GetComponentInChildren<SphereCollider>();
        _collider.enabled = false;
        canBurst = false; //this *could* be true, but it fixes itself in Update so let's just not
        var pSMain = partSys.main; //"We CaN't ChAnGe ThE cOlOr UnLeSs YoU mAkE tHiS a VaR" -Unity
        pSMain.startColor = new Color(145, 145, 145, 255); //set start color to a middle-ish gray
        UpdateColor(PlayerManager.CurrentColor); //set to whatever the manager says it should be
    }

    //this coroutine brought to you by Grant Ross. Thanks Grant! -Noah
    private IEnumerator DoBurst()
    {
        _collider.radius = 0.1f;
        _collider.enabled = true;
        while (_collider.radius < burstSize)
        {
            _collider.radius += .2f;
            yield return new WaitForSeconds(.05f);
            //tweaked these two numbers until they more or less lined up with the particles
        }
        _collider.enabled = false;
    }

    void Update(){
        //if our particle system is not active and we're not paused, then our particle system is ready
        partSysReady = !partSys.IsAlive() && !PlayerManager.isPaused;
        //if the system is ready and we push the right button, play the burst and do the burst coroutine
        if(partSysReady && PlayerManager.CurrentColor != ColorIDs.Colors.NONE && Input.GetButtonDown("Fire1")){
            partSys.Play();
            StartCoroutine(DoBurst());
        }
        //finally, if our particle system is ready (again, that just means our system is not already
        //active and we aren't paused) and we push the other button, switch our color out
        if(partSysReady && Input.GetButtonDown("Fire2")){
            PlayerManager.SwitchColor();
            UpdateColor(PlayerManager.CurrentColor);
        }
    }

    //take the passed ColorIDs.Colors value, make it an actual Color, generate a godforsaken Gradient
    //from it with full opacity to full transparency, and set that Gradient to the particle system's
    //official "color over lifetime" value for the particles
    void UpdateColor(ColorIDs.Colors c){
        Color newColor = PlayerManager.MakeColor(c);
        Gradient grad = new Gradient();
        //what's with this indenting, you ask? yeah, it's weird, but there's a reason
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(newColor, 0.0f), //incomplete array
                new GradientColorKey(newColor, 1.0f)}, //completed first-argument array
            new GradientAlphaKey[]{ new GradientAlphaKey(1.0f, 0.0f), //incomplete array
                new GradientAlphaKey(0.0f, 1.0f)}); //completed second-argument array
        var col = partSys.colorOverLifetime; //"We CaN't ChAnGe ThE cOlOr UnLeSs YoU mAkE tHiS a VaR" -Unity
        col.color = grad;
    }
}
