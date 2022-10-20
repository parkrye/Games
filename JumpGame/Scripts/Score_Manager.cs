using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Manager : MonoBehaviour
{
    public Text textScore;
    private int score;

    private void Awake()
    {
        UpdateScore(0);
    }

    public void UpdateScore(int value)
    {
        score += value;
        UpdateUi();
    }
    
    public int GetScore()
    {
        return score;
    }

    public void UpdateUi()
    {
        textScore.text = score.ToString("f0");
    }

    // PlayerPrefs�� �ְ� ������ ����صΰ� highscoreKey Ű�� ����
    string highscoreKey = "HighScore";
    public int GetHighScore()
    {
        int highScore = PlayerPrefs.GetInt(highscoreKey);
        return highScore;
    }

    public void SetHighScore(int curScore)
    {
        if (curScore > GetHighScore())
        {
            PlayerPrefs.SetInt(highscoreKey, curScore);
        }
    }
}
