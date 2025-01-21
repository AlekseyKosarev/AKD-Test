using System;
using UnityEngine.InputSystem;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    public PlayerInputActions playerControls;
    private InputAction _move;
    private InputAction _look;
    private InputAction _interact;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerControls = new PlayerInputActions();
        _move = playerControls.Player.Move;
        _look = playerControls.Player.Look;
        _interact = playerControls.Player.Interact;
        _move.Enable();
        _look.Enable();
        _interact.Enable();
        _interact.performed += Interact;
    }

    private void OnDisable()
    {
        _move.Disable();
        _look.Disable();
        _interact.Disable();
    }
    void Update()
    {
        var moveDir = _move.ReadValue<Vector2>();
        var lookDir = _look.ReadValue<Vector2>();
        
        Debug.Log(moveDir);
        Debug.Log(lookDir);
        
    }

    void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("interact!");
    }
}
