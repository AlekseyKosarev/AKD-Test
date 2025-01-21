using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputController: MonoBehaviour
{
    private IControllable _controllable;
    private PlayerInputActions _playerInputActions;
    private InputAction _move;
    private InputAction _look;
    private InputAction _interact;
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();

        _controllable = GetComponent<IControllable>();
        if (_controllable == null)
            throw new Exception("CharacterInputController requires an IControlable component");
    }
    
    private void OnEnable()
    {
        _playerInputActions = new PlayerInputActions();
        
        _move = _playerInputActions.Player.Move;
        _look = _playerInputActions.Player.Look;
        _interact = _playerInputActions.Player.Interact;
        
        _move.Enable();
        _look.Enable();
        _interact.Enable();
        
        _interact.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        _move.Disable();
        _look.Disable();
        _interact.Disable();
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("interact");
        _controllable.Interact();
    }
    
    private void Update()
    {
        ReadLook();
        ReadMovement();
    }
    
    private void ReadLook()
    {
        Vector2 lookVector = _look.ReadValue<Vector2>();
        _controllable.Look(lookVector);
    }
    
    private void ReadMovement()
    {
        Vector2 moveVector = _move.ReadValue<Vector2>();
        _controllable.Move(moveVector);
    }   
}