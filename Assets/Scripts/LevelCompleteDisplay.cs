using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteDisplay : MonoBehaviour
{

    [SerializeField] Text highScoreText;

    [SerializeField] Text scoreForTimeText;
    [SerializeField] Text scoreForPowerText;
    [SerializeField] Text scoreText;

    [SerializeField] Button nextLevelButton;
    [SerializeField] Button mainMenuButton;

    private void Start()
    {
        highScoreText.gameObject.SetActive(GameManager.Instance.IsHighScore);

        scoreForTimeText.text = $"Score for time: {GameManager.Instance.ScoreForTime()}";
        scoreForPowerText.text = $"Score for remaining power: {GameManager.Instance.ScoreForPower()}";
        scoreText.text = $"Total score: {GameManager.Instance.Score}";

        nextLevelButton.onClick.AddListener(GameManager.Instance.StartNextLevel);
        mainMenuButton.onClick.AddListener(GameManager.Instance.ToMainMenu);

        GameManager.Instance.Save();
    }

}
