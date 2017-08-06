using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class ScoreDisplay : MonoBehaviour
{

    [SerializeField] Text scoreText;
    [SerializeField] Text levelText;
    [SerializeField] Text timeText;

    private void FixedUpdate()
    {
        scoreText.text = $"Score: {GameManager.Instance.Score}";
        levelText.text = $"Level: {GameManager.Instance.Level}";
        timeText.text = $"Time: {GameManager.Instance.LevelTime.ToString("0.00")}";
    }

}

