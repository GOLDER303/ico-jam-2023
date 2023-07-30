using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Vector3[] obstaclePartsPositions;
    [SerializeField] private ObstaclePartSO[] obstaclePartsSOs;
    [SerializeField] private GameObject obstaclePartPrefab;
    [SerializeField] private float moveSpeed = 5f;

    public List<PlayerShapeSO> fittingPlayerShapesSos { get; } = new List<PlayerShapeSO>();

    public void Start()
    {
        for (int i = 0; i < obstaclePartsPositions.Length; i++)
        {
            ObstaclePartSO obstaclePartSO = obstaclePartsSOs[Random.Range(0, obstaclePartsSOs.Length)];

            GameObject spawnedObstaclePart = Instantiate(obstaclePartPrefab, obstaclePartsPositions[i] + transform.position, Quaternion.identity);

            spawnedObstaclePart.GetComponent<MeshFilter>().mesh = obstaclePartSO.mesh;
            spawnedObstaclePart.GetComponent<MeshRenderer>().material = obstaclePartSO.material;

            spawnedObstaclePart.transform.SetParent(transform);

            fittingPlayerShapesSos.Add(obstaclePartSO.fittingPlayerShapeSO);
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }
}
