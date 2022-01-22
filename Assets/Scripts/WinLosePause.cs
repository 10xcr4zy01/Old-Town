using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLosePause : MonoBehaviour
{
    [SerializeField]
    GameObject
        winCanvas,
        loseCanvas,
        pauseCanvas,
        player,
        winCondition;
    float timer;

    private void Awake()
    {
        timer = 3f;
    }

    private void Update()
    {
        if (winCanvas.activeSelf == false && loseCanvas.activeSelf == false && pauseCanvas.activeSelf == false)
        {
            Time.timeScale = 1f;
        }

        //Lose
        if (player.GetComponent<Player>().health == 0)
        {
            player.GetComponent<Player>().money = 0;
            Open(2);
        }

        //Win
        if (winCondition.activeSelf == true)
        {
            timer -= Time.deltaTime;
            if (GameObject.FindWithTag("Enemy") == true)
            {
                timer = 2f;
            }
            if (timer < 0)
            {
                Open(1);
            }
        }
        
    }

    private void Open (int i)
    {
        switch (i)
        {
            case 1:
                winCanvas.SetActive(true);
                Pause();
                break;
            case 2: 
                loseCanvas.SetActive(true);
                Pause();
                break;
            case 3:
                pauseCanvas.SetActive(true);
                Pause();
                break;
        }
    }

    private void Pause ()
    {
        Time.timeScale = 0.000001f; 
    }
}
