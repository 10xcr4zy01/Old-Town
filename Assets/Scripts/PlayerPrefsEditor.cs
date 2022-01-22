using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsEditor : MonoBehaviour
{
    public int maxHealth, money, maxBullets, bankAmount;
    public float speed, attackSpeed;
    public Player player;
    public void SetPlayerPreps()
    {
        PlayerPrefs.SetFloat("speed", speed);
        PlayerPrefs.SetInt("maxHealth", maxHealth);
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetFloat("attackSpeed", attackSpeed);
        PlayerPrefs.SetInt("maxBullets", maxBullets);
        PlayerPrefs.SetInt("bankAmount", bankAmount);
        player.LoadStats();
    }
    public void SetPlayerPrebsDefault()
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
        player.LoadStats();

    }
}
