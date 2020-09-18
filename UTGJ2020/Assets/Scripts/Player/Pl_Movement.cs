using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pl_Movement : MonoBehaviour
{
    private InputMaster controls;

    private void Awake() {
        controls = new InputMaster();
        controls.Player.Movement.performed += context => Move(context.ReadValue<Vector2>());
    }

    void Move(Vector2 direction) {
        Debug.Log("Player wants to move " + direction);
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }
}
