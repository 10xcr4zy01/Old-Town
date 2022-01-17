using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTilemap : MonoBehaviour
{
    [SerializeField] private GameObject activeObject, player;
    [SerializeField] private int postionToMove;
    Transform cameraTransform;
    
    private void Start()
    {
       cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        activeObject.SetActive(true);
        MovingCamera();
    }

    private void MovingCamera ()
    {
        player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 0.5f);
        Vector3 moveToPosition = new Vector3(0, postionToMove, -10);
        cameraTransform.transform.position = Vector3.Lerp(transform.position, moveToPosition, 2f);
    }

}
