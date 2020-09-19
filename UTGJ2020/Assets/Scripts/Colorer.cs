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
    }

    private IEnumerator DoBurst()
    {
        _collider.radius = 0.1f;
        _collider.enabled = true;
        while (_collider.radius < burstSize)
        {
            _collider.radius += .1f;
            yield return new WaitForSeconds(.1f);
        }
        _collider.enabled = false;
    }

    void Update(){
        canBurst = !partSys.IsAlive();
        if(canBurst && Input.GetButtonDown("Fire1")){
            partSys.Play();
            StartCoroutine(DoBurst());
        }
    }
}
