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

    private float score;
    private float scoreGrowthRate = 10f;

    private void Update()
    {
        score += (scoreGrowthRate * Time.deltaTime);

        scoreText.text = "Score: " + Mathf.Floor(score);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        inGameUI.SetActive(false);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }
}
