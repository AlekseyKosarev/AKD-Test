using UnityEngine;

public class HoldObject: MonoBehaviour, IHoldable
{
    [SerializeField] private float forceDrop = 500;
    private Transform _saveHandPosition;
    private Rigidbody _rb;
    private bool _isHold = false;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void Hold(Transform playerHandPosition)
    {
        _saveHandPosition = playerHandPosition;
        _isHold = true;
        transform.SetParent(playerHandPosition); 
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        _rb.isKinematic = true;
    }
    public void Drop()
    {
        if (_isHold)
        {
            _isHold = false;
            transform.SetParent(null);
            _rb.isKinematic = false;
            _rb.AddForce(_saveHandPosition.forward * forceDrop);
        }
    }
}