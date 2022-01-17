using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTilemap : MonoBehaviour
{
    [SerializeField] private GameObject spawner, player, gate;
    [SerializeField] private int postionToMove;

    Transform cameraTransform;

    bool istriggered;
    int timeTrigger;


    private void Awake()
    {
       istriggered = false;
       timeTrigger = 0;
       cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void FixedUpdate()
    {
        if (GameObject.FindWithTag("Enemy") == false && timeTrigger == 0)
        {
            istriggered = true;
            gate.SetActive(false);
        }
        else 
        {
            gate.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && istriggered == true && timeTrigger == 0)
        {     
            MovingCamera();
            gate.SetActive(true);
            spawner.SetActive(true);
            timeTrigger += 1;
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            
            istriggered = false;

        }
    }

    private void MovingCamera ()
    {
        player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 1f);
        Vector3 moveToPosition = new Vector3(0, postionToMove, -10);
        cameraTransform.transform.position = Vector3.Lerp(transform.position, moveToPosition, 2f);
    }
}
