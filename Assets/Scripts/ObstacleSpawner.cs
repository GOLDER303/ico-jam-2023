using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Obstacle[] obstaclePrefabs;
    [SerializeField] private float spawnDelay = 1f;


    private void Start()
    {
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
