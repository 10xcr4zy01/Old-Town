using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickEvent : MonoBehaviour
{
    public AudioSource click;
    public void Exits ()
    {
        click.Play();
        Application.Quit();
    }

    public void SwtichScenes(string nameScene)
    {
        click.Play();
        SceneManager.LoadScene(nameScene);
    }

    public void SwtichScenesAndSave(string nameScene)
    {
        click.Play();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player>().SaveStats();
        SceneManager.LoadScene(nameScene);
    }

    public void OpenMenu (GameObject menuObject)
    {
        click.Play();
        menuObject.SetActive(true);
    }

    public void CloseMenu(GameObject menuObject)
    {
        click.Play();
        menuObject.SetActive(false);
    }

}
