using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ColorBillBoard : MonoBehaviour
{
    private Camera cam;
    public bool hasRotation;
    private SpriteRenderer _spriteRenderer;
    
    // Blank sprites
    public Sprite frontSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite backSprite;

    // Colored sprites
    public Sprite frontColoredSprite;
    public Sprite leftColoredSprite;
    public Sprite rightColoredSprite;
    public Sprite backColoredSprite;
    public ColorIDs.Colors objectColor;

    private Sprite[] spriteArr;
    private Sprite[] coloredSpriteArr;
    private Sprite[] currentSpriteArr;   

    public bool active;
    private float _baseRotation;


    private enum Dirs {
        Front,  // 0
        Left,   // 1
        Right,  // 2
        Back    // 3
    }

    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _baseRotation = transform.eulerAngles.y;
        cam = PlayerManager.player.GetComponentInChildren<Camera>();
        spriteArr = new Sprite[] { frontSprite, leftSprite, rightSprite, backSprite };
        coloredSpriteArr = new Sprite[] { frontColoredSprite, leftColoredSprite, rightColoredSprite, backColoredSprite };
        chooseCurrentArray(); // Syncs the used array to the active state
    }

    private void Update() {
        transform.forward = new Vector3(cam.transform.forward.x, transform.forward.y, cam.transform.forward.z);
        
        if (hasRotation) {
            var rotate = transform.eulerAngles.y - _baseRotation;
            if (rotate < 0) rotate += 360;
            if (rotate > 180) rotate -= 360;

            //currentSpriteArr stands in for actual directional sprites
            _spriteRenderer.sprite = rotate < 45 && rotate > -45 ? currentSpriteArr[(int) Dirs.Front] :
                rotate >= 45 && rotate <= 135 ? currentSpriteArr[(int)Dirs.Left] :
                rotate <= -45 && rotate >= -135 ? currentSpriteArr[(int)Dirs.Right] : currentSpriteArr[(int)Dirs.Back];
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("Burst") && PlayerManager.CurrentColor == objectColor) {
            active = !active;
            chooseCurrentArray();
        }
    }

    private void chooseCurrentArray() {
        if (active) {
            currentSpriteArr = coloredSpriteArr;
            print("Array is ColorArray!");
        } else {
            currentSpriteArr = spriteArr;
        }
    }  
}
