using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorer : MonoBehaviour
{
    public ParticleSystem partSys;
    public bool canBurst;
    private SphereCollider _collider;
    public float burstSize;
    
    void Start(){
        partSys = GetComponent<ParticleSystem>();
        _collider = GetComponentInChildren<SphereCollider>();
        _collider.enabled = false;
        canBurst = false;
        UpdateColor(PlayerManager.CurrentColor);
    }

    private IEnumerator DoBurst()
    {
        _collider.radius = 0.1f;
        _collider.enabled = true;
        while (_collider.radius < burstSize)
        {
            _collider.radius += .2f;
            yield return new WaitForSeconds(.05f);
        }
        _collider.enabled = false;
    }

    void Update(){
        //if our particle system is not active and we have an actual color selected, then we can burst
        canBurst = !partSys.IsAlive() && PlayerManager.CurrentColor != ColorIDs.Colors.NONE;
        //if we can burst and push the right button, play the burst and do the burst coroutine
        if(canBurst && Input.GetButtonDown("Fire1")){
            partSys.Play();
            StartCoroutine(DoBurst());
        }
        if(Input.GetButtonDown("Fire2")){
            PlayerManager.SwitchColor();
            UpdateColor(PlayerManager.CurrentColor);
        }
    }

    void UpdateColor(ColorIDs.Colors c){
        Color newColor = PlayerManager.MakeColor(c);
        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(newColor, 0.0f),
                new GradientColorKey(newColor, 1.0f)},
            new GradientAlphaKey[]{ new GradientAlphaKey(1.0f, 0.0f), 
                new GradientAlphaKey(0.0f, 1.0f)});
        var col = partSys.colorOverLifetime;
        col.color = grad;
    }
}
