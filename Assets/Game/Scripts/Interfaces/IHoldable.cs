using UnityEngine;

public interface IHoldable
{
    void Hold(Transform playerHandPosition);
    void Drop();
}