using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int maxPositionsToSide = 1;
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float positionDistance = 5f;
    [SerializeField] GameManager gameManager;

    public Queue<PlayerShapeSO> playerShapeChangeQueue { get; } = new Queue<PlayerShapeSO>();

    private Rigidbody playerRigidBody;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private PlayerInput playerInput;
    private Vector3 targetPlayerPosition;
    private PlayerShapeSO currentPlayerShapeSO;
    private ParticleSystem currentParticleSystem;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        targetPlayerPosition = transform.position;
    }

    private void OnEnable()
    {
        Obstacle.OnObstacleSpawn += AddShapeChangeToQueue;
    }
    private void OnDisable()
    {
        Obstacle.OnObstacleSpawn += AddShapeChangeToQueue;
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

    private void AddShapeChangeToQueue(PlayerShapeSO nextPlayerShapeSO)
    {
        if (playerShapeChangeQueue.Count == 0)
        {
            StartParticleSystem(nextPlayerShapeSO.particleSystem);
        }

        playerShapeChangeQueue.Enqueue(nextPlayerShapeSO);
    }

    public void ChangeShape()
    {
        Destroy(currentParticleSystem.gameObject);

        PlayerShapeSO nextPlayerShapeSO = playerShapeChangeQueue.Dequeue();

        meshFilter.mesh = nextPlayerShapeSO.mesh;
        meshRenderer.material = nextPlayerShapeSO.material;

        currentPlayerShapeSO = nextPlayerShapeSO;

        if (playerShapeChangeQueue.Count > 0)
        {
            StartParticleSystem(playerShapeChangeQueue.Peek().particleSystem);
        }
    }

    private void StartParticleSystem(ParticleSystem particleSystem)
    {
        currentParticleSystem = Instantiate(particleSystem, transform.position, Quaternion.identity);

        currentParticleSystem.transform.SetParent(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ObstaclePart"))
        {
            if (other.GetComponent<ObstaclePart>().obstaclePartSO.fittingPlayerShapeSO != currentPlayerShapeSO)
            {
                playerInput.DeactivateInput();
                gameManager.GameOver();
            }
        }
    }
}
