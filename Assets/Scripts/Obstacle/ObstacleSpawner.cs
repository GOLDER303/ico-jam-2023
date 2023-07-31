using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Obstacle[] obstaclePrefabs;
    [SerializeField] private float initialSpawnDelay = 2f;

    public float spawnDelay { get; set; }

    private bool shouldSpawn = true;

    private void OnEnable()
    {
        PlayerController.OnPlayerDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= HandlePlayerDeath;
    }

    private void Start()
    {
        spawnDelay = initialSpawnDelay;
        StartCoroutine(SpawnObstacleCoroutine());
    }

    private void HandlePlayerDeath()
    {
        shouldSpawn = false;
    }

    private IEnumerator SpawnObstacleCoroutine()
    {
        while (shouldSpawn)
        {
            Obstacle spawnedObstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
