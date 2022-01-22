using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sheriff : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] Text txtAbout;
    [SerializeField] GameObject btnClaim;
    [SerializeField] Player player;
    int state;
    private void Awake ()
    {
        
    }

    private void Update()
    {
        state = PlayerPrefs.GetInt("QuestState");
        ListQuest(state);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            menu.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            menu.SetActive(false);
    }

    private void ListQuest (int state)
    {
        switch (state)
        {
            case 0:
                txtAbout.text = "Clear map Cemetery!";
                if (PlayerPrefs.GetFloat("cemetery") < 9998)
                {
                    btnClaim.GetComponent<Button>().interactable = true;
                }
                else
                {
                    btnClaim.GetComponent<Button>().interactable = false;
                }
                break;
            case 1:
                txtAbout.text = "Clear map Cemetery less than 180s!";
                if (PlayerPrefs.GetFloat("cemetery") < 180)
                {
                    btnClaim.GetComponent<Button>().interactable = true;
                }
                else
                {
                    btnClaim.GetComponent<Button>().interactable = false;
                }
                break;
            case 2:
                txtAbout.text = "Clear map Cemetery less than 100s!";
                if (PlayerPrefs.GetFloat("cemetery") < 100)
                {
                    btnClaim.GetComponent<Button>().interactable = true;
                }
                else
                {
                    btnClaim.GetComponent<Button>().interactable = false;
                }
                break;
            case 3:
                txtAbout.text = "Defeat Coyote!";
                if (PlayerPrefs.GetFloat("coyote") < 9998)
                {
                    btnClaim.GetComponent<Button>().interactable = true;
                }
                else
                {
                    btnClaim.GetComponent<Button>().interactable = false;
                }
                break;
            case 4:
                txtAbout.text = "Defeat Coyote less than 180s";
                if (PlayerPrefs.GetFloat("coyote") < 180)
                {
                    btnClaim.GetComponent<Button>().interactable = true;
                }
                else
                {
                    btnClaim.GetComponent<Button>().interactable = false;
                }
                break;
            case 5:
                txtAbout.text = "Defeat Coyote less than 100s";
                if (PlayerPrefs.GetFloat("coyote") < 100)
                {
                    btnClaim.GetComponent<Button>().interactable = true;
                }
                else
                {
                    btnClaim.GetComponent<Button>().interactable = false;
                }
                break;
            case 6:
                txtAbout.text = "You have completed all the quests";
                btnClaim.SetActive(false);
                break;

        }
    }


    public void ClaimButtonClickEvent ()
    {
        player.GetComponent<Player>().money += 100;
        PlayerPrefs.SetInt("money", player.GetComponent<Player>().money);
        PlayerPrefs.SetInt("QuestState", state+1);
        state = PlayerPrefs.GetInt("QuestState");
    }



 
}

