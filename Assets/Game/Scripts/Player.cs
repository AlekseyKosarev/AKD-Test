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
    public void Construct(Camera camera, CharacterController characterController)
    {
        _playerCamera = camera;
        _characterController = characterController;
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

    public void Interact(Vector2 clickPos)
    {
        Ray ray = _playerCamera.ScreenPointToRay(clickPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                Debug.Log("Interacted with: " + hit.collider.name);
            }
            else
            {
                Debug.Log("No interactable object found.");
            }
        }
        else
        {
            Debug.Log("Raycast did not hit any object.");
        }
    }

}