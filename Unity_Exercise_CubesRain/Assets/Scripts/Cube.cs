using System;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Cube : MonoBehaviour
{
    public Transform Transform { get; private set; }
    public bool HaveHitted { get; private set; }

    public event Action Hitted;
    public event Action<Cube> Dying;

    private void Awake()
    {
        Transform = GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (HaveHitted == false)
        {
            HaveHitted = true;
            Hitted?.Invoke();
            Dying?.Invoke(this);
        }
    }

    public void SetActive(bool state) 
    {
        gameObject.SetActive(state);
    }

    public void InitializeHitted(bool haveHitted)
    {
        HaveHitted = haveHitted;
    }

    public void InitializePosition(Vector3 position)
    {
        Transform.position = position;
    }
}
