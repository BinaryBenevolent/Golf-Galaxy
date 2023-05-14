using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private TMP_Text finalScore;

    [SerializeField] private BallController ball;

    private void Start()
    {
        scoreText.text = Convert.ToString(ball.ShootCount);
    }

    public void SetScore(int score)
    {
        scoreText.text = Convert.ToString(score);
    }

    public void SetFinalScore(BallController ball)
    {
        finalScore.text = "Shoot Count : " + ball.ShootCount;
    }
}
