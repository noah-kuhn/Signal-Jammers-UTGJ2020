using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pl_Movement : MonoBehaviour
{
    public float moveSpeed = 15f;
    [Tooltip("Changes the speed at which the player falls. Consequence of using CharacterController and not Rigidbody")]
    public float gravityScale = 3;
    public float jumpForce = 15f;

    private float verticalVelocity;
    private bool jumping;
    private CharacterController controller;
    private Vector3 moveDirection;
    
    private void Awake() {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate() {
        if (controller.isGrounded) {
            verticalVelocity = Physics.gravity.y * gravityScale;
            Debug.Log(controller.isGrounded);
            if (Input.GetKeyDown(KeyCode.Space)) {
                verticalVelocity = jumpForce;
                Debug.Log(verticalVelocity);
            }
        } else {
            verticalVelocity += Physics.gravity.y * gravityScale * Time.deltaTime;
        }
                
        
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized * moveSpeed;
        moveDirection.y += verticalVelocity;
        moveDirection = transform.TransformDirection(moveDirection);
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ground") {

        }
    }
}
