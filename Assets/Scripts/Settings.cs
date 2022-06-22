using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Sprite[] previewPlayerArray;
    [SerializeField] Color[] previewPlatformArray;
    [SerializeField] Color[] previewTrailArray;

    [SerializeField] Slider volumeSlider;

    [SerializeField] Sprite[] musicIconArray;
    SpriteRenderer musicIcon;

    const string MASTER_VOLUME_KEY = "Master Volume";
    const float MIN_VOLUME = 0f;
    const float MAX_VOLUME = 1f;

    SpriteRenderer previewPlayer;
    int previewPlayerIndex;

    SpriteRenderer previewPlatform;
    int previewPlatformIndex;

    SpriteRenderer previewTrail;
    int previewTrailIndex;

    private void Start()
    {
        previewPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        previewPlayer.sprite = previewPlayerArray[PlayerPrefs.GetInt("PlayerSprite")];
        previewPlayerIndex = PlayerPrefs.GetInt("PlayerSprite");

        previewPlatform = GameObject.FindGameObjectWithTag("Platform").GetComponent<SpriteRenderer>();
        previewPlatform.color = previewPlatformArray[PlayerPrefs.GetInt("PlatformColor")];
        previewPlatformIndex = PlayerPrefs.GetInt("PlatformColor");

        previewTrail = GameObject.FindGameObjectWithTag("Trail").GetComponent<SpriteRenderer>();
        previewTrail.color = previewTrailArray[PlayerPrefs.GetInt("TrailColor")];
        previewTrailIndex = PlayerPrefs.GetInt("TrailColor");

        volumeSlider.value = GetMasterVolume();

        musicIcon = GameObject.FindGameObjectWithTag("Music Icon").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();
        musicPlayer.SetVolume(volumeSlider.value);

        if (volumeSlider.value == 0)
        {
            musicIcon.sprite = musicIconArray[0];
        }
        else
        {
            musicIcon.sprite = musicIconArray[1];
        }
    }

    public void SaveAndExit()
    {
        SetMasterVolume(volumeSlider.value);
        FindObjectOfType<LevelController>().LoadMainMenu();
    }

    public void NextPlayerSprite()
    {
        previewPlayerIndex++;

        if (previewPlayerIndex >= previewPlayerArray.Length)
        {
            previewPlayerIndex = 0;
        }

        previewPlayer.sprite = previewPlayerArray[previewPlayerIndex];

        PlayerPrefs.SetInt("PlayerSprite", previewPlayerIndex);
    }

    public void PreviousPlayerSprite()
    {
        previewPlayerIndex--;

        if (previewPlayerIndex < 0)
        {
            previewPlayerIndex = previewPlayerArray.Length - 1;
        }

        previewPlayer.sprite = previewPlayerArray[previewPlayerIndex];
        PlayerPrefs.SetInt("PlayerSprite", previewPlayerIndex);
    }

    public void NextPlatformColor()
    {
        previewPlatformIndex++;

        if (previewPlatformIndex >= previewPlatformArray.Length)
        {
            previewPlatformIndex = 0;
        }

        previewPlatform.color = previewPlatformArray[previewPlatformIndex];

        PlayerPrefs.SetInt("PlatformColor", previewPlatformIndex);
    }

    public void PreviousPlatformColor()
    {
        previewPlatformIndex--;

        if (previewPlatformIndex < 0)
        {
            previewPlatformIndex = previewPlatformArray.Length - 1;
        }

        previewPlatform.color = previewPlatformArray[previewPlatformIndex];
        PlayerPrefs.SetInt("PlatformColor", previewPlatformIndex);
    }

    public void NextTrailColor()
    {
        previewTrailIndex++;

        if (previewTrailIndex >= previewTrailArray.Length)
        {
            previewTrailIndex = 0;
        }

        previewTrail.color = previewTrailArray[previewTrailIndex];

        PlayerPrefs.SetInt("TrailColor", previewTrailIndex);
    }

    public void PreviousTrailColor()
    {
        previewTrailIndex--;

        if (previewTrailIndex < 0)
        {
            previewTrailIndex = previewTrailArray.Length - 1;
        }

        previewTrail.color = previewTrailArray[previewTrailIndex];
        PlayerPrefs.SetInt("TrailColor", previewTrailIndex);
    }

    public static void SetMasterVolume(float volume)
    {
        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master volume is out of range!");
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }
}
