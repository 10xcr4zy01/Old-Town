using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickEvent : MonoBehaviour
{
    // Start is called before the first frame update

    public void Exits ()
    {
        Application.Quit();
    }

    public void SwtichScenes(string nameScrene)
    {
        SceneManager.LoadScene(nameScrene);
    }


}
