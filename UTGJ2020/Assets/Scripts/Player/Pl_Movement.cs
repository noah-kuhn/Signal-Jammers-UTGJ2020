﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pl_Movement : MonoBehaviour
{
    public float moveSpeed = 10f;
    [Tooltip("Changes the speed at which the player falls. Consequence of using CharacterController and not Rigidbody")]
    public float gravityScale = 3;
    public float jumpForce = 15f;
    private bool _jumping;
    private float verticalVelocity;
    private CharacterController controller;
    private Vector3 moveVector;
    private Vector3 moveDirection;
    private float terminalVel;
    private Animator _anim;
    [SerializeField] private float _coyoteTime = 0.1f;
    private float timeLeftPlatform;
    //private AudioManager _audioManager;

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private Direction dir;

    private enum State
    {
        Idle,
        Walk,
        Jump,
        Fall,
        Landed
    }

    private State _state;
    
    private void Awake() {
        controller = GetComponent<CharacterController>();
        terminalVel = Physics.gravity.y * gravityScale;
        _anim = GetComponentInChildren<Animator>();
        _state = State.Idle;
        //_audioManager = FindObjectOfType<AudioManager>();
        dir = Direction.Up;
        if(!AudioManager.Instance.musicSource.isPlaying) AudioManager.Instance.PlayMusic(AudioManager.SoundIDs.MAIN_MUSIC);
    }

    private void Update()
    {
        if (!Input.GetAxis("Vertical").Equals(0))
        {
            if (Input.GetAxis("Vertical") > 0) dir = Direction.Up;
            else dir = Direction.Down;
        }
        else if (!Input.GetAxis("Horizontal").Equals(0))
        {
            if (Input.GetAxis("Horizontal") > 0) dir = Direction.Right;
            else dir = Direction.Left;
        }

        var lastState = _state;
        if (controller.isGrounded)
        {
            _state = moveDirection.x.Equals(0) && moveDirection.z.Equals(0) ? State.Idle : State.Walk;
            if (lastState == State.Idle && _state == State.Walk) AudioManager.Instance.PlaySound(AudioManager.SoundIDs.WALK);
            else if (_state != State.Walk) AudioManager.Instance.FadeSound(AudioManager.SoundIDs.WALK);
            
        }
        else _state = moveDirection.y > 0 ? State.Jump : State.Fall;
        
        _anim.SetBool("walk", _state == State.Walk);
        _anim.SetBool("jump", _state == State.Jump);
        _anim.SetBool("fall", _state == State.Fall);
        _anim.SetBool("vertical", dir == Direction.Up || dir == Direction.Down);
        _anim.SetBool("left", dir == Direction.Left);
        _anim.SetBool("up", dir == Direction.Up);
        
        
        
        if (lastState == State.Fall && controller.isGrounded)
        {
        AudioManager.Instance.PlaySound(AudioManager.SoundIDs.LAND);
        }
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
