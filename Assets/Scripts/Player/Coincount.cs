using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coincount : MonoBehaviour
{
    Player player;

    int coin;

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
        coin  = player.coin;
        txt.text = coin.ToString();
    }
}
