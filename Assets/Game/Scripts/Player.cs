using System;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IControllable
{
    [SerializeField] private Transform handPosition;
    [SerializeField] private float interactionRadius = 2f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSpeed = 2f;
    [SerializeField] private float maxVerticalAngle = 80f;
    private float _verticalRotation = 0f;
    
    private Camera _playerCamera;
    private CharacterController _characterController;
    private IHoldable _currentlyHeldObject;

    [Inject]
    public void Construct(Camera camera, CharacterController characterController)
    {
        _playerCamera = camera;
        _characterController = characterController;
    }

    public void Move(Vector2 moveInput)
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        move = _playerCamera.transform.TransformDirection(move);
        move.y = -10f;
        _characterController.Move(move * moveSpeed * Time.deltaTime);
    }

    public void Look(Vector2 lookInput)
    {
        float mouseX = lookInput.x * lookSpeed;
        float mouseY = lookInput.y * lookSpeed;

        // Обновляем вертикальный угол с ограничением
        _verticalRotation -= mouseY;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -maxVerticalAngle, maxVerticalAngle);

        // Применяем вращение
        _playerCamera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
        transform.Rotate(0, mouseX, 0); // Вращаем игрока по горизонтали
    }

    public void Interact(Vector2 clickPos)
    {
        if (_currentlyHeldObject != null)
            return;
        Ray ray = _playerCamera.ScreenPointToRay(clickPos);
        RaycastHit hit;
    
        if (Physics.Raycast(ray, out hit))
        {
            Hold(hit);
        }
    }

    private void Hold(RaycastHit hit)
    {
        float distance = Vector3.Distance(_playerCamera.transform.position, hit.collider.transform.position);
        if (distance > interactionRadius)
            return;
        
        IHoldable holdable = hit.collider.GetComponent<IHoldable>();
        if (holdable != null)
        {
            holdable.Hold(handPosition);
            _currentlyHeldObject = holdable;
            Debug.Log("Interacted with: " + hit.collider.name);
        }
    }

    public void Drop()
    {
        if (_currentlyHeldObject == null)
            return;
        _currentlyHeldObject.Drop();
        _currentlyHeldObject = null;
    }
}