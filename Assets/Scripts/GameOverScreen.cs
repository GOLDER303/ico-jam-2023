using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;

    public void Setup(int score)
    {
        finalScoreText.text = "Final Score: " + score;
    }
}