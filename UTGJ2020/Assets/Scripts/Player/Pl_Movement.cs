using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pl_Movement : MonoBehaviour
{
    private InputMaster controls;
    private Rigidbody rb;
    [Tooltip("Player speed")]
    public float moveSpeed = 50f;
    private void Awake() {
        controls = new InputMaster();
        rb = GetComponent<Rigidbody>();
        controls.Player.Movement.performed += context => Move(context.ReadValue<Vector2>());
    }

    void Move(Vector2 direction) {
        Debug.Log("Player wants to move " + direction);
        rb.AddForce(direction * moveSpeed);
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }
}
