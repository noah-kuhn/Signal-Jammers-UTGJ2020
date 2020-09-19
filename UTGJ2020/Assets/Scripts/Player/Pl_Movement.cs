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
    private CharacterController controller;
    private Vector3 moveVector;
    private Vector3 moveDirection;
    private float terminalVel;
    private Animator _anim;
    [SerializeField] private float _coyoteTime = 0.1f;
    private float timeLeftPlatform;
    
    private void Awake() {
        controller = GetComponent<CharacterController>();
        terminalVel = Physics.gravity.y * gravityScale;
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _anim.SetBool("walk", !(moveDirection.x + moveDirection.z).Equals(0));
        _anim.SetBool("left", moveVector.x < 0);
        _anim.SetBool("up", moveVector.z > 0);
    }

    private void FixedUpdate()
    {
        if (controller.isGrounded) {
            timeLeftPlatform = 0;
            if (Input.GetButton("Jump")) {
                verticalVelocity = jumpForce;
            }
        } else {
            // Gradually accelerates a falling player to terminal velocity
            timeLeftPlatform += Time.deltaTime;
            verticalVelocity += terminalVel * Time.deltaTime;
            Mathf.Clamp(verticalVelocity, 0, terminalVel);
            if(timeLeftPlatform < _coyoteTime && Input.GetButton("Jump")){
                verticalVelocity = jumpForce;
            }
        }

        moveVector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveDirection = moveVector.normalized * moveSpeed;
        moveDirection.y += verticalVelocity;
        moveDirection = transform.TransformDirection(moveDirection);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
