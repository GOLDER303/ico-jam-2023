using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int maxPositionsToSide = 1;
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float positionDistance = 5f;

    private Rigidbody playerRigidBody;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Vector3 targetPlayerPosition;
    private PlayerShapeSO nextPlayerShapeSO;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        targetPlayerPosition = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPlayerPosition, moveSpeed * Time.deltaTime);
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 inputVector = inputValue.Get<Vector2>();

        if (inputVector.x == 0 || Mathf.Abs(targetPlayerPosition.x + (inputVector.x * positionDistance)) > maxPositionsToSide * positionDistance)
        {
            return;
        }

        targetPlayerPosition.x = targetPlayerPosition.x + (inputVector.x * positionDistance);
    }

    public void AssignNextShape(PlayerShapeSO nextPlayerShapeSO)
    {
        //TODO: start particle system

        this.nextPlayerShapeSO = nextPlayerShapeSO;
    }

    public void ChangeShape()
    {
        meshFilter.mesh = nextPlayerShapeSO.mesh;
        meshRenderer.material = nextPlayerShapeSO.material;
    }
}
