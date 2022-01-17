using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCount : MonoBehaviour
{
    Player player;

    int currentBullet;
    int maxBullet;  

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
        maxBullet = player.maxBullets;
        currentBullet = player.bullet;
        text = currentBullet.ToString() + "/" + maxBullet.ToString();
        txt.text = text;
    }
}
