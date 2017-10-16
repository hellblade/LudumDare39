using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] LevelCompleteDisplay levelCompleteDisplayPrefab;
    [SerializeField] GameOverDisplay gameOverDisplay;

    public static GameManager Instance { get; private set; }

    public int Level { get; private set; }

    public int Score { get; private set; }

    public float LevelTime { get; private set; }

    public float WorstPossibleTime { get; set; }

    public int MaxHealth = 200;
    public int MaxPower = 500;

    int scoreAtStart;

    public bool UseRelativeControls { get; set; } = true;

    public void ToggleRelativeControls()
    {
        UseRelativeControls = !UseRelativeControls;
    }

    public int HighScore
    {
        get { return PlayerPrefs.GetInt("HighScore", 0); }
        private set { PlayerPrefs.SetInt("HighScore", value); }
    }

    public int BestLevel
    {
        get { return PlayerPrefs.GetInt("BestLevel", 0); }
        private set { PlayerPrefs.SetInt("BestLevel", value); }
    }

    private void Awake()
    {
        // If there's already one we don't need this (used for testing in Unity, so can use playmode well)
        if (GameManager.Instance)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
        Instance = this;
    }

    public void Reset()
    {
        Level = 0;
        Score = 0;
    }

    public void LevelCompleted()
    {
        Time.timeScale = 0;

        var player = FindObjectOfType<PlayerController>();
        MaxPower = player.GetComponent<Power>().maxPower;
        MaxHealth = player.GetComponent<Health>().maxHealth;


        AddScore(ScoreForPower());
        AddScore(ScoreForTime());

        Instantiate(levelCompleteDisplayPrefab);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        Instantiate(gameOverDisplay);
    }

    public void Save()
    {
        if (IsHighScore)
        {
            HighScore = Score;
        }

        if (BestLevel < Level)
        {
            BestLevel = Level;
        }
    }

    public void StartNextLevel()
    {
        LevelTime = 0;
        scoreAtStart = Score;
        Time.timeScale = 1;
        Level++;
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void RetryLevel()
    {
        LevelTime = 0;
        Score = scoreAtStart;
        Time.timeScale = 1;
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void ToMainMenu()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }

    public bool IsHighScore
    {
        get { return Score > HighScore; }
    }

    private void FixedUpdate()
    {
        LevelTime += Time.deltaTime;
    }

    public int ScoreForPower()
    {
        var player = FindObjectOfType<PlayerController>();
        var power = player.GetComponent<Power>();
        
        return (int)power.CurrentPower * Level;
    }

    public int ScoreForTime()
    {
        return (int)(Mathf.Max(0, WorstPossibleTime - LevelTime)) * Level;
    }

}
