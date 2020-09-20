using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour
{
    public Mesh onMesh;
    public Mesh offMesh;
    public Material onMaterial;
    public Material offMaterial;
    public Waterfall sister1; 
    public Waterfall sister2;
    public ColorIDs.Colors platformColor;
    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;
    public MeshRenderer _renderer;
    public bool active;
    public bool isSwitching;

    // Start is called before the first frame update
    public void Awake() {
        // Make sure color is actually assigned
        if (platformColor == ColorIDs.Colors.NONE) {
            Debug.LogError("Platform color must be assigned");
        }

        _meshFilter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();
        _meshFilter.mesh = onMesh;
        _meshCollider.isTrigger = !active;
        _meshFilter.mesh = active ? onMesh : offMesh;
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material = active ? onMaterial : offMaterial;
    }

    public void OnTriggerEnter(Collider other) {
        Switch(other);
        if(!sister1.isSwitching)
            sister1.Switch(other);
        if(!sister2.isSwitching)
        sister2.Switch(other);
    }

    public void Switch(Collider other) {
        isSwitching = true;
        if (other.transform.CompareTag("Burst") && PlayerManager.CurrentColor == platformColor) {
            active = !active;
            _meshCollider.isTrigger = !active;
            _meshFilter.mesh = active ? onMesh : offMesh;
            _renderer.material = active ? onMaterial : offMaterial;
        }
        isSwitching = false;
    }
}
