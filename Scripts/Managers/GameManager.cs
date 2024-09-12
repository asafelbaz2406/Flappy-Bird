using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }   
    public delegate void OnScoreAction();
    public OnScoreAction OnScoreEvent;

    public delegate void OnDeathEvent();
    public OnDeathEvent OnPlayerDeathEvent; 

    public bool gameOver;

    private string input;
    private int _score;

    private void Awake() {
        if(Instance != null)
        {
            Debug.LogError("There's more than one GameManager!" + Instance + " - " + transform);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateScore()
    {
       // print("ScoreEvent");
        OnScoreEvent?.Invoke();
    }

    public void ReadStringInput(string s)
    {
        input = s;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public string GetInput()
    {
        return input;
    }

    public int GetScore()
    {
        return _score;
    }

    public void SetScore(int score)
    {
        _score = score;
    }

}
