using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaletteIcon : MonoBehaviour
{
    public ColorIDs.Colors color;
    private Image image;
    
    // Used to make sure certain code isn't run when it's not relevant
    private bool activated;


    private void Start() {
        image = GetComponent<Image>();
        image.enabled = false;
        activated = false;
        image.color = PlayerManager.MakeColor(color);
    }

    private void Update() {
        if (!activated && PlayerManager.AvailableColors.Contains(color)) {
            Activate();
        }

        if (activated) {
            if (PlayerManager.CurrentColor == color) {
                Focus();
            } else {
                Defocus();
            }
        }
    }
    public void Activate() {
        image.enabled = true;
        activated = true;
    }

    public void Focus() {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.8f);
    }
    // Makes the button transparent
    public void Defocus() {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.3f);
    }
}
