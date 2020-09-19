using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorer : MonoBehaviour
{
    public ParticleSystem partSys;
    public bool partSysReady; //is our particle system ready to burst?
    [Range(0, 1)] public float sphereTransparency;
    private SphereCollider _collider; //the actual trigger for the burst
    private MeshRenderer _renderer;
    private Material _material;
    public float burstSize;

    void Start(){
        partSys = GetComponent<ParticleSystem>();
        _collider = GetComponentInChildren<SphereCollider>();
        _renderer = GetComponentInChildren<MeshRenderer>();
        _material = _renderer.material;
        _collider.enabled = false;
        _renderer.enabled = false;
        partSysReady = false; //this *could* be true, but it fixes itself in Update so let's just not
        var pSMain = partSys.main; //"We CaN't ChAnGe ThE cOlOr UnLeSs YoU mAkE tHiS a VaR" -Unity
        pSMain.startColor = new Color(145, 145, 145, 255); //set start color to a middle-ish gray
        UpdateColor(PlayerManager.CurrentColor); //set to whatever the manager says it should be
    }

    

    void Update(){
        //if our particle system is not active and we're not paused, then our particle system is ready
        partSysReady = !partSys.IsAlive() && !PlayerManager.isPaused && !_renderer.enabled;
        //if the system is ready and we push the right button, play the burst and do the burst coroutine
        if(partSysReady && PlayerManager.CurrentColor != ColorIDs.Colors.NONE && Input.GetButtonDown("Fire1")){
            partSys.Play();
            StartCoroutine(DoBurst());
        }
        //finally, if our particle system is ready (again, that just means our system is not already
        //active and we aren't paused) and we push the other button, switch our color out
        if(Input.GetButtonDown("Fire2")){
            StartCoroutine(UpdatePartSys());
        }
    }
    
    //this coroutine brought to you by Grant Ross. Thanks Grant! -Noah
    private IEnumerator DoBurst()
    {
        const float START_DIAM = 0.1f;
        Color originalSphereColor = _material.color;
        //_collider.radius = START_DIAM / 2;
        _renderer.transform.localScale = new Vector3(START_DIAM, START_DIAM, START_DIAM);
        _collider.enabled = true;
        _renderer.enabled = true;

        while (_renderer.transform.localScale.x <= burstSize || _material.color.a > 0)
        {
            // Steadily grows the sphere's diameter until it reaches the correct size
            float expansionRate = (burstSize / 30); // This number is ~arbitrary based on the particle speed
            _renderer.transform.localScale += new Vector3(expansionRate, expansionRate, expansionRate);
            
            // Steadily decreases the sphere's opacity
            float alphaChange = sphereTransparency / 29;
            _material.color -= new Color(0f, 0f, 0f, alphaChange);
            
            yield return new WaitForSeconds(.01f);
        }
        _collider.enabled = false;
        _renderer.enabled = false;
        _material.color = originalSphereColor;
    }

    // Updates the color of the particle system once the particles disappear
    private IEnumerator UpdatePartSys() {
        // Honestly I don't know why a lambda operator works here but unity says I need one
        yield return new WaitUntil(() => partSysReady);
        PlayerManager.SwitchColor();
        UpdateColor(PlayerManager.CurrentColor);

    }

    //take the passed ColorIDs.Colors value, make it an actual Color, generate a godforsaken Gradient
    //from it with full opacity to full transparency, and set that Gradient to the particle system's
    //official "color over lifetime" value for the particles
    public void UpdateColor(ColorIDs.Colors c){
        Color newColor = PlayerManager.MakeColor(c);
        Gradient grad = new Gradient();
        //what's with this indenting, you ask? yeah, it's weird, but there's a reason
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(newColor, 0.0f), //incomplete array
                new GradientColorKey(newColor, 1.0f)}, //completed first-argument array
            new GradientAlphaKey[]{ new GradientAlphaKey(1.0f, 0.0f), //incomplete array
                new GradientAlphaKey(0.0f, 1.0f)}); //completed second-argument array
        var col = partSys.colorOverLifetime; //"We CaN't ChAnGe ThE cOlOr UnLeSs YoU mAkE tHiS a VaR" -Unity
        col.color = grad;

        newColor.a = sphereTransparency;
        _material.SetColor("_Color", newColor);
    }

    
}
