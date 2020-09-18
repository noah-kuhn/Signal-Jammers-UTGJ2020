using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pl_Movement : MonoBehaviour
{
    public float moveSpeed = 50f;
    [Tooltip("Changes the speed at which the player falls. Consequence of using CharacterController and not Rigidbody")]
    public float gravityScale = 3;
    private CharacterController controller;
    private Vector3 moveDirection;
    
    private void Awake() {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate() {
        
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * moveSpeed;
        moveDirection.y += Physics.gravity.y * gravityScale;
        controller.Move(moveDirection * Time.deltaTime);
    }

}
