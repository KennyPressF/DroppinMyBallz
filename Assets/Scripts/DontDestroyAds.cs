using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAds : MonoBehaviour
{
    private void Awake()
    {
        SetUpAdsSingleton();
    }

    private void SetUpAdsSingleton()
    {
        int numberOfAdsManagers = GameObject.FindGameObjectsWithTag("Finish").Length;

        if (numberOfAdsManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
