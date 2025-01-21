using UnityEngine;

public interface IControllable
{
    void Move(Vector2 moveInput);
    void Look(Vector2 lookInput);
    void Interact(Vector2 clickPosition);
}