using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public int numberOfLives;
    public int currentScore;
    [SerializeField] int scoreValue;
    AdsDisplay adsDisplay;

    private void Awake()
    {
        SetUpSingleton();
        SetDefaultVolume();
    }

    private void SetUpSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        adsDisplay = FindObjectOfType<AdsDisplay>();
        ResetScore();
        numberOfLives = 5;
    }

    public void LoseLife()
    {
        numberOfLives -= 1;
        if (numberOfLives == 0)
        {
            adsDisplay.ShowAd();
            numberOfLives = 5;
        }
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void AddToScore()
    {
        currentScore = currentScore + scoreValue;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

    public void SetHighScore()
    {
        if (PlayerPrefs.GetInt("Highscore") < currentScore)
        {
            PlayerPrefs.SetInt("Highscore", currentScore);
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("Highscore", 0);
    }

    public void SetDefaultVolume()
    {
        if (PlayerPrefs.HasKey("Master Volume"))
        {
            return;
        }
        else
        {
            PlayerPrefs.SetFloat("Master Volume", 0.8f);
        }
    }
}
