using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerPos;
    public float cameraSize = 8;

    // Start is called before the first frame update
    void Start()
    {
        //Initialise camera on player
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y, -10);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y, -10);
    }
}
