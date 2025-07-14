using System;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    [SerializeField] private GameObject[] _dyingZones;

    public event Action<Cube> Entered;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Cube cube))
            Entered?.Invoke(cube);
    }
}