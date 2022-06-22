using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float timerSpeed = 2f;
    [SerializeField] float timeGain = 5f;

    Slider timer;
    LevelController levelController;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        gameSession = FindObjectOfType<GameSession>();
        timer = GetComponent<Slider>();
        timer.value = timer.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        ReduceTime();
        TimesUp();
    }

    private void ReduceTime()
    {
        timer.value = timer.value - timerSpeed * Time.deltaTime;
    }

    public void ResetTimer()
    {
        timer.value = timer.value + timeGain;
    }

    private void TimesUp()
    {
        if (timer.value <= 0)
        {
            gameSession.SetHighScore();
            levelController.LoadGameOver();
        }
    }
}
