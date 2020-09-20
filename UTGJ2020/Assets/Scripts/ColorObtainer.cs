using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObtainer : MonoBehaviour
{
    [Tooltip("Color to be obtained when this item is picked up")]
    public ColorIDs.Colors color;

    private void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("Player")) 
        {
            Debug.Log($"calling AddColor with {color}...");
            PlayerManager.AddColor(color);
            Debug.Log($"calling UpdateColor with {color}...");
            //the child in index 1 should be the particle system
            PlayerManager.player.transform.GetChild(1).gameObject.GetComponent<Colorer>().UpdateColor(color);
        }
    }

    //TODO: When color is obtained, add color UI element to pallete UI. 
    //Give tooltip on how to use colors/switch colors for first and second respectively?

}
