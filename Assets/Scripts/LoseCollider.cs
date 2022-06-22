using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour
{
    GameSession gameSession;
    LevelController levelController;
    AdsDisplay adsDisplay;

    // Start is called before the first frame update
    void Start()
    {
        adsDisplay = FindObjectOfType<AdsDisplay>();
        gameSession = FindObjectOfType<GameSession>();
        levelController = FindObjectOfType<LevelController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameSession.LoseLife();       
        gameSession.SetHighScore();
        levelController.LoadGameOver();
    }
}
