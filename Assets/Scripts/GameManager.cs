using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private float scoreGrowthRate = 5f;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private float spawnDelayDecreasingRate = .1f;
    [SerializeField] private float minSpawnDelay = .7f;

    public float currentSpawnDelay => obstacleSpawner.spawnDelay;

    private float score;
    private bool gameOver = false;

    private void OnEnable()
    {
        PlayerController.OnPlayerDeath += GameOver;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= GameOver;
    }

    private void Update()
    {
        score += (scoreGrowthRate * Time.deltaTime);
        scoreText.text = "Score: " + Mathf.Floor(score);

        if (obstacleSpawner.spawnDelay > minSpawnDelay)
        {
            obstacleSpawner.spawnDelay -= spawnDelayDecreasingRate * Time.deltaTime;
        }
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOverScreen.SetActive(true);
            gameOverScreen.GetComponent<GameOverScreen>().Setup(Mathf.FloorToInt(score));

            inGameUI.SetActive(false);

            gameOver = true;
        }
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(1);
    }
}
