using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public TileAttributes ta;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(ta.getXTile() + 0.5f, ta.getYTile() + 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(ta.getXTile() + 0.5f, ta.getYTile() + 0.5f, 0);
    }
}
