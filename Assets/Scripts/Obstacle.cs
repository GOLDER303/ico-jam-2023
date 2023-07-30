using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;


    private void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }
}
