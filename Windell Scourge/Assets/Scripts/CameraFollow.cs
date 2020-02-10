using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Rigidbody2D player;
    public float smoothSpeed = 0.125f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float blend = 1f - Mathf.Pow(1f - 0.1f, Time.deltaTime*30f);
        Vector3 desiredPos = new Vector3(player.position.x, player.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, desiredPos, blend);
        transform.position = desiredPos;
    }
}
