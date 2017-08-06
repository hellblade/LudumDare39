using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverDisplay : MonoBehaviour
{

    [SerializeField] Button retryButton;
    [SerializeField] Button mainMenuButton;

    private void Start()
    {
        retryButton.onClick.AddListener(GameManager.Instance.RetryLevel);
        mainMenuButton.onClick.AddListener(GameManager.Instance.ToMainMenu);
    }

}
