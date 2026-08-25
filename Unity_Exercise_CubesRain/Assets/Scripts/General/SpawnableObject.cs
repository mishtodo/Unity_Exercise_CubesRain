using System;
using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour
{
    public event Action<SpawnableObject> OnDying;

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
