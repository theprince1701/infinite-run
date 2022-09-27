using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Scene = UnityEngine.SceneManagement.Scene;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public enum GameStates
    {
        PreGame,
        GameStarted,
        GameOver
    }

    [SerializeField] private Player player;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text gameOverScoreText;
    [SerializeField] private Text bestScoreText;
    
    public GameStates State { get; private set; }
    
    public float Score { get; set; }
    
    public Player Player => player;

    private int _bestScore;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _bestScore = PlayerPrefs.GetInt("BestScore");
        GameUI.Instance.EnablePanel("PreGame");
    }

    private void Update()
    {
        if (State == GameStates.PreGame)
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            
            UpdateGameState(GameStates.GameStarted);
            GameUI.Instance.EnablePanel("InGame");
        }
        else if (State == GameStates.GameStarted)
        {
            scoreText.text = ((int)Score).ToString();
        }
    }

    public void PlayGame()
    {
        Scene scene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(scene.buildIndex);
    }

    public void UpdateGameState(GameStates state)
    {
        if (state == State)
            return;
        
        State = state;

        switch (State)
        {
            case GameStates.GameOver:
                GameOver();
                break;
            
            case GameStates.GameStarted:

                break;
            
            case GameStates.PreGame:
                GameUI.Instance.EnablePanel("PreGame");
                break;
        }
    }

    private void GameOver()
    {
        if (Score > _bestScore)
        {
            PlayerPrefs.SetInt("BestScore", (int)Score);
            _bestScore = (int) Score;
        }

        bestScoreText.text = "BEST: " + _bestScore;
        gameOverScoreText.text = "SCORE: " + (int)Score;
        GameUI.Instance.EnablePanel("GameOver");
    }
}
