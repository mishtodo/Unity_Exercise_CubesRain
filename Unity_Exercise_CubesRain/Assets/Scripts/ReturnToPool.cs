using System;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    [SerializeField] private GameObject _dyingZone;

    public event Action<Cube> Entered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cube cube))
            Entered?.Invoke(cube);
    }
}
