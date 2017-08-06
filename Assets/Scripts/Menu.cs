using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] Text highScoreDisplay;

    GameManager manager;

	// Use this for initialization
	void Start ()
    {
        manager = FindObjectOfType<GameManager>();
        manager.Reset();

        highScoreDisplay.text = $"High Score: {manager.HighScore}\r\nBest Level: {manager.BestLevel}";
	}

    public void NewGame()
    {
         manager.StartNextLevel();
    }

    public void Quit()
    {
        Application.Quit();
    }

}
