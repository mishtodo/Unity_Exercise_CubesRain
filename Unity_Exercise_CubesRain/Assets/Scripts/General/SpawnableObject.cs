using System;
using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour
{
    public event Action<SpawnableObject> OnDying;

    protected virtual void OnEnable() { }

    public void InitializeVelocity(Vector3 value)
    {
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = value;
            rb.angularVelocity = value;
        }
    }

    public void InitializePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void InitializeRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    protected void NotifyDying()
    {
        OnDying?.Invoke(this);
    }
}