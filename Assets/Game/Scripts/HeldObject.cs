using UnityEngine;
public class HeldObject: MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Pick up object"); 
    }
}