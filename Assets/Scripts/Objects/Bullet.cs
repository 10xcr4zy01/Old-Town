using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject Player;

    Coroutine flashRoutine;

    void OnTriggerEnter2D(Collider2D col)
    {

        Monster monster = col.gameObject.GetComponent<Monster>();
        if (monster != null)
        {
            monster.ChangeHealth(-1);
        }
        //Flash();
        Destroy(Instantiate(effect, transform.position, transform.rotation), 0.2f);
        Destroy(gameObject);
        
    }

}
