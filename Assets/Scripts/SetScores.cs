using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetScores : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;

    private int score;
    private int highscore;

    void Start()
    {
        score = PlayerPrefs.GetInt("currentScore");
        if (score > PlayerPrefs.GetInt("highscore"))
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);
        } 
        else highscore = PlayerPrefs.GetInt("highscore");

        DisplayScore();
        DisplayHighscore();
    }

    private void DisplayScore()
    {
        scoreText.text = "YOUR SCORE: " + score.ToString();
    }

    private void DisplayHighscore()
    {
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }
}
