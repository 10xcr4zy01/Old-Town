using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bartender : MonoBehaviour
{
    [SerializeField] GameObject menu, txtStatus, btAS, btAM, btHP;
    [SerializeField] Player player;
    [SerializeField] Text attackSpeed, ammo, hp;

    private void Update()
    {
        UpdatePlayerStat();
        statIsMax();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            menu.SetActive(true);
            txtStatus.GetComponent<Text>().text = "";
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            menu.SetActive(false);
    }

    public void UpgradeStats (string statName)
    {
        if (player.money < 100)
        {
            txtStatus.SetActive(true);
            txtStatus.GetComponent<Text>().text = "NOT ENOUGH GOLD!";
        }
        else
        {
            switch (statName)
            {
                case "as":
                    player.attackSpeed -= 0.4f;
                    break;
                case "ammo":
                    player.maxBullets += 1;
                    break;
                case "hp":
                    player.maxHealth += 1;
                    break;

            }
            player.money -= 100; 
            player.SaveStats();
            txtStatus.SetActive(true);
            txtStatus.GetComponent<Text>().text = "UPGRADE SUCCESSFUL!";

        }

    }

    public void UpdatePlayerStat ()
    {
        attackSpeed.text = player.attackSpeed.ToString();
        ammo.text = player.maxBullets.ToString();
        hp.text = player.maxBullets.ToString();
    }

    void statIsMax ()
    {
        if (player.attackSpeed <= 0.49f)
        {
            btAS.SetActive(false);
        }
        else btAS.SetActive(true);
        if (player.maxBullets == 9)
        {
            btAM.SetActive(false);
        }
        else btAM.SetActive(true);
        if (player.maxHealth == 9)
        {
            btHP.SetActive(false);
        }
        else btHP.SetActive(true);
    }
}
