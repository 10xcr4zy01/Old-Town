using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public int maxSpawn;
    public GameObject monsterPrefabs;
    public float time = 2.0f;
    float timer;


    int currentSpawn;

    // Start is called before the first frame update
    void Start()
    {
        currentSpawn = maxSpawn;
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > time && currentSpawn > 0)
        {
            GameObject monster = Instantiate(monsterPrefabs, transform.position, transform.rotation);
            timer = 0;
            currentSpawn -= 1;
        }
    }
}
