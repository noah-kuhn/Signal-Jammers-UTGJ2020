using System;
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
            if (Input.GetButton("Jump")) {
                verticalVelocity = jumpForce;
                Debug.Log(verticalVelocity);
            }
        } else {
            float terminalVel = Physics.gravity.y * gravityScale;
            // Gradually accelerates a falling player to terminal velocity
            verticalVelocity += terminalVel * Time.deltaTime;
            Mathf.Clamp(verticalVelocity, 0, terminalVel);
        }
                
        
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized * moveSpeed;
        moveDirection.y += verticalVelocity;
        moveDirection = transform.TransformDirection(moveDirection);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
