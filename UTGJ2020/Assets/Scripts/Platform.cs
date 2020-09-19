using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Mesh onMesh;
    public Mesh offMesh;
    public ColorIDs.Colors platformColor;
    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;
    public bool active;
    
    // Start is called before the first frame update
    private void Awake()
    {
        // Make sure color is actually assigned
        if (platformColor == ColorIDs.Colors.NONE) {
            Debug.LogError("Platform color must be assigned");
        }

        _meshFilter= GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();
        _meshFilter.mesh = onMesh;
        active = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Burst") && PlayerManager.CurrentColor == platformColor) // add && Player color == platform color
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
