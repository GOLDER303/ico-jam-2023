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

    private float score;

    private void Update()
    {
        score += (scoreGrowthRate * Time.deltaTime);

        scoreText.text = "Score: " + Mathf.Floor(score);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<GameOverScreen>().Setup(Mathf.FloorToInt(score));

        inGameUI.SetActive(false);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }
}
