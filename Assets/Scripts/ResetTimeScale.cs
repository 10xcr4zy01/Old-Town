using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTimeScale : MonoBehaviour
{
    
    void Awake()
    {
        Time.timeScale = 1.0f;
        SetPlayerPrebsDefault();
    }

    void SetPlayerPrebsDefault ()
    {
        if (PlayerPrefs.GetFloat("speed") == 0)
        {
            PlayerPrefs.SetFloat("speed", 0.8f);
            PlayerPrefs.SetInt("maxHealth", 3);
            PlayerPrefs.SetInt("money", 0);
            PlayerPrefs.SetFloat("attackSpeed", 1.2f);
            PlayerPrefs.SetInt("maxBullets", 3);
            PlayerPrefs.SetFloat("cemetery", 9999);
            PlayerPrefs.SetFloat("coyote", 9999);
            PlayerPrefs.SetFloat("bankAmount", 0);
            PlayerPrefs.SetInt("QuestState", 0);
            PlayerPrefs.SetFloat("volume", 0);
        }

    }

}
