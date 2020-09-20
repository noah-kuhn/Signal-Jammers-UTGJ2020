﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Mesh onMesh;
    public Mesh offMesh;
    public Material onMaterial;
    public Material offMaterial;
    public ColorIDs.Colors platformColor;
    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;
    private MeshRenderer _renderer;
    public bool active;
    
    // Start is called before the first frame update
    public void Awake()
    {
        // Make sure color is actually assigned
        if (platformColor == ColorIDs.Colors.NONE) {
            Debug.LogError("Platform color must be assigned");
        }

        _meshFilter= GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();
        _meshFilter.mesh = onMesh;
        _meshCollider.isTrigger = !active;
        _meshFilter.mesh = active ? onMesh : offMesh;
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material = active ? onMaterial : offMaterial;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Burst") && PlayerManager.CurrentColor == platformColor) 
        {
            active = !active;
            _meshCollider.isTrigger = !active;
            _meshFilter.mesh = active ? onMesh : offMesh;
            _renderer.material = active ? onMaterial : offMaterial;
        }
    }
}
