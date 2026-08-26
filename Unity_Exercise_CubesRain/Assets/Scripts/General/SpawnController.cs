using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;

    private void OnEnable()
    {
        _cubeSpawner.ObjectReleased += SpawnBombAtPosition;
    }

    private void OnDisable()
    {
        _cubeSpawner.ObjectReleased -= SpawnBombAtPosition;
    }

    public void SpawnBombAtPosition(Vector3 position)
    {
        _bombSpawner.Spawn(position);
    }
}