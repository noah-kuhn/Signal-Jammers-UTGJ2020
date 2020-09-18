using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Mesh onMesh;
    public Mesh offMesh;
    //public Colors color;
    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;
    public bool active = false;
    
    // Start is called before the first frame update
    private void Awake()
    {
        _meshFilter= GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();
        _meshFilter.mesh = onMesh;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Burst")) // add && Player color == platform color
        {
            active = !active;
            _meshCollider.enabled = active;
            _meshFilter.mesh = active ? onMesh : offMesh;
        }
    }
}
