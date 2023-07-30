using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShapeChangeParticlesTrigger : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Obstacle obstacle = other.GetComponent<Obstacle>();

            PlayerShapeSO nextPlayerShapeSO = obstacle.fittingPlayerShapesSos[Random.Range(0, obstacle.fittingPlayerShapesSos.Count)];

            playerController.AssignNextShape(nextPlayerShapeSO);
        }
    }
}
