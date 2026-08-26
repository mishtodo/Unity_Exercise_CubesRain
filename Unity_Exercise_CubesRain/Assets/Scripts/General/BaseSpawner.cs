using System;
using UnityEngine;

public abstract class BaseSpawner : MonoBehaviour
{
    public virtual event Action MetersChanged;
    public abstract int TotalSpawned { get; }
    public abstract int TotalCreated { get; }
    public abstract int ActiveCount { get; }
}