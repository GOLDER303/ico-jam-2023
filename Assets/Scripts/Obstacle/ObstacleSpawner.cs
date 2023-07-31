using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Obstacle[] obstaclePrefabs;
    [SerializeField] private float initialSpawnDelay = 2f;

    public float spawnDelay { get; set; }

    private void Start()
    {
        spawnDelay = initialSpawnDelay;
        StartCoroutine(SpawnObstacleCoroutine());
    }

    private IEnumerator SpawnObstacleCoroutine()
    {
        while (true)
        {
            Obstacle spawnedObstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
