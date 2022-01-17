using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartCount : MonoBehaviour
{
    private Player player;

    int maxheart;
    int currentheart;

    Text txt;
    string text;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        txt = GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        maxheart = player.maxHealth;
        currentheart = player.health;
        text = currentheart.ToString() + "/" + maxheart.ToString();
        txt.text = text;
    }
}
