using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIcon : MonoBehaviour
{
    public SpriteRenderer sRenderer;
    // Start is called before the first frame update
    private void Start() {
        sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.enabled = false;        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            sRenderer.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            sRenderer.enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
