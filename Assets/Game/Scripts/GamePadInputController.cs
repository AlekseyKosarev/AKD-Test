using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GamePadInputController: MonoBehaviour
{
    private IControllable _controllable;
    private PlayerInputActions _playerInputActions;
    private InputAction _move;
    private InputAction _look;
    private InputAction _interact;
    private InputAction _drop;
    
    [Inject]
    public void Construct(IControllable controllable)
    {
        _controllable = controllable;
    }
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }
    
    private void OnEnable()
    {
        _playerInputActions = new PlayerInputActions();
        
        _move = _playerInputActions.Player.Move;
        _look = _playerInputActions.Player.Look;
        _interact = _playerInputActions.Player.Interact;
        _drop = _playerInputActions.Player.Drop;
            
        _move.Enable();
        _look.Enable();
        _interact.Enable();
        _drop.Enable();
        
        _interact.performed += OnInteractPerformed;
        _drop.performed += OnDropPerformed;
    }

    private void OnDisable()
    {
        _move.Disable();
        _look.Disable();
        _interact.Disable();
        _drop.Disable();
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        var position = context.ReadValue<Vector2>();
        _controllable.Interact(position);
    }
    private void OnDropPerformed(InputAction.CallbackContext context)
    {
        _controllable.Drop();
    }
    
    private void Update()
    {
        ReadLook();
    }
    private void FixedUpdate()
    {
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