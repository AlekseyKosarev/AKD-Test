using System;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IControllable
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSpeed = 2f;
    [SerializeField] private float verticalRotation = 0f;
    [SerializeField] private const float maxVerticalAngle = 80f;

    private Camera _playerCamera;
    private CharacterController _characterController;

    [Inject]
    public void Construct(Camera camera)
    {
        _playerCamera = camera;
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 moveInput)
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        move = _playerCamera.transform.TransformDirection(move);
        move.y = 0;
        _characterController.Move(move * moveSpeed * Time.deltaTime);
    }

    public void Look(Vector2 lookInput)
    {
        float mouseX = lookInput.x * lookSpeed;
        float mouseY = lookInput.y * lookSpeed;

        // Обновляем вертикальный угол с ограничением
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);

        // Применяем вращение
        _playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.Rotate(0, mouseX, 0); // Вращаем игрока по горизонтали
    }

    public void Interact()
    {
        Debug.Log("interact click");
    }
}