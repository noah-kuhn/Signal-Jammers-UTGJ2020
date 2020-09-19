using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{

    public Camera cam;
    public bool hasRotation;
    private SpriteRenderer _spriteRenderer;
    public Sprite frontSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite backSprite;
    private float _baseRotation;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _baseRotation = transform.eulerAngles.y;
    }

    private void Update()
    {
        transform.forward  = new Vector3(cam.transform.forward.x, transform.forward.y, cam.transform.forward.z);
        
        if (hasRotation)
        {
            var rotate = transform.eulerAngles.y-_baseRotation;
            if (rotate < 0) rotate += 360;
            if (rotate > 180) rotate -= 360;
            
            _spriteRenderer.sprite = rotate < 45 && rotate > -45 ? frontSprite :
                rotate >= 45 && rotate <= 135 ? leftSprite :
                rotate <= -45 && rotate >= -135 ? rightSprite : backSprite;
        }
    }
}