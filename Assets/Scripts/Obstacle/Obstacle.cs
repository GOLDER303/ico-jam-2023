using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static Action<PlayerShapeSO> OnObstacleSpawn;

    [SerializeField] private Vector3[] obstaclePartsPositions;
    [SerializeField] private ObstaclePartSO[] obstaclePartsSOs;
    [SerializeField] private GameObject obstaclePartPrefab;
    [SerializeField] private float moveSpeed = 5f;

    public List<PlayerShapeSO> fittingPlayerShapesSos { get; } = new List<PlayerShapeSO>();

    private void OnEnable()
    {
        PlayerController.OnPlayerDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        Destroy(gameObject);
    }

    public void Start()
    {
        for (int i = 0; i < obstaclePartsPositions.Length; i++)
        {
            ObstaclePartSO obstaclePartSO = obstaclePartsSOs[UnityEngine.Random.Range(0, obstaclePartsSOs.Length)];

            GameObject spawnedObstaclePart = Instantiate(obstaclePartPrefab, obstaclePartsPositions[i] + transform.position, Quaternion.identity);

            spawnedObstaclePart.GetComponent<MeshFilter>().mesh = obstaclePartSO.mesh;
            spawnedObstaclePart.GetComponent<MeshRenderer>().material = obstaclePartSO.material;
            spawnedObstaclePart.GetComponent<ObstaclePart>().obstaclePartSO = obstaclePartSO;

            spawnedObstaclePart.transform.SetParent(transform);

            fittingPlayerShapesSos.Add(obstaclePartSO.fittingPlayerShapeSO);
        }

        OnObstacleSpawn?.Invoke(fittingPlayerShapesSos[UnityEngine.Random.Range(0, fittingPlayerShapesSos.Count)]);
    }

    private void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        if (transform.position.z < -10f)
        {
            Destroy(gameObject);
        }
    }
}
