using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Platform : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    [SerializeField] float startSpeed;
    [SerializeField] float currentSpeed;
    [SerializeField] float speedIncrease;

    [SerializeField] Color[] platformColorArray;

    Player player;
    Timer timer;
    GameSession gameSession;
    AudioSource audioSource;

    SpriteRenderer mySpriteRenderer;

    Transform nextPosition;
    int nextPositionIndex;

    bool inGame;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Game"))
        {
            inGame = true;
        }
        else { inGame = false; }

        mySpriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>();
        timer = FindObjectOfType<Timer>();
        gameSession = FindObjectOfType<GameSession>();
        audioSource = GetComponent<AudioSource>();

        nextPosition = positions[0];
        currentSpeed = startSpeed;

        SetPlatformColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (inGame == true)
        {
            Move();
        }
    }

    private void Move()
    {
        if (transform.position == nextPosition.position)
        {
            nextPositionIndex++;
            if (nextPositionIndex >= positions.Length)
            {
                nextPositionIndex = 0;
            }
            nextPosition = positions[nextPositionIndex];
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, nextPosition.position, currentSpeed * Time.deltaTime);
        }
    }

    public void SpeedUp()
    {
        currentSpeed = currentSpeed + speedIncrease;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (inGame == true)
        {
            audioSource.volume = Settings.GetMasterVolume();
            audioSource.Play();
            SpeedUp();
            player.BounceBack();
            timer.ResetTimer();
            gameSession.AddToScore();
        }
    }

    private void SetPlatformColor()
    {
        mySpriteRenderer.color = platformColorArray[PlayerPrefs.GetInt("PlatformColor")];
    }
}
