using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShapeChangeTrigger : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            playerController.ChangeShape();
        }
    }
}
