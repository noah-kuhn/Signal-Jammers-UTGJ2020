using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pl_Movement : MonoBehaviour
{
    public float moveSpeed = 10f;
    [Tooltip("Changes the speed at which the player falls. Consequence of using CharacterController and not Rigidbody")]
    public float gravityScale = 3;
    public float jumpForce = 15f;

    private float verticalVelocity;
    private bool jumping;
    private Rigidbody _rigidbody;
    //private CapsuleCollider _groundCheck;
    private bool _grounded;
    private CharacterController controller;
    private Vector3 moveDirection;
    private float terminalVel;
    
    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        //_groundCheck = transform.GetChild(0).GetComponent<CapsuleCollider>();
        controller = GetComponent<CharacterController>();
        terminalVel = Physics.gravity.y * gravityScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            _grounded = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            _grounded = false;
        }
    }

    private void FixedUpdate()
    {
        if (controller.isGrounded) {
            verticalVelocity = Physics.gravity.y * gravityScale;
            if (Input.GetButton("Jump")) {
                verticalVelocity = jumpForce;
            }
        } else {
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
