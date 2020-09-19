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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Burst")) // add && Player color == platform color
        {
            active = !active;
            _meshCollider.isTrigger = !active;
            _meshFilter.mesh = active ? onMesh : offMesh;
        }
    }

    private void OnCollisionEnter(Collision other)
    {

    }
}
