using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private string _compareTagToCollision = "Environment";

    private Coroutine _coroutine;

    public bool HaveHitted { get; private set; }

    public event Action Hitted;
    public event Action<Cube> Dying;

    private void OnCollisionEnter(Collision collision)
    {
        if (HaveHitted == false && collision.collider.CompareTag(_compareTagToCollision))
        {
            HaveHitted = true;
            Hitted?.Invoke();
            RestartCoroutine();
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
        transform.position = position;
    }

    public void InitializeRotation(Quaternion rotation)
    {
       transform.rotation = rotation;
    }

    public void StopCoroutine()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void RestartCoroutine()
    {
        _coroutine = StartCoroutine(WaitBeforeDestroyCube());
    }

    private IEnumerator WaitBeforeDestroyCube()
    {
        float minDestroyDelay = 2;
        float maxDestroyDelay = 5;
        float RandomDelay = UnityEngine.Random.Range(minDestroyDelay, maxDestroyDelay);
        var wait = new WaitForSecondsRealtime(RandomDelay);

        yield return wait;
        Dying?.Invoke(this);
    }
}
