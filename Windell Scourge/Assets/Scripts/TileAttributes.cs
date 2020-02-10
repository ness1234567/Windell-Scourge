using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAttributes : MonoBehaviour
{
    private Collider2D myCollider;

    [SerializeField]
    private float offsetx = 0;
    [SerializeField]
    private float offsety = 0;

    [SerializeField]
    private int X_Tile = 0;
    [SerializeField]
    private int Y_Tile = 0;

    public int getXTile()
    {
        return X_Tile;
    }

    public int getYTile()
    {
        return Y_Tile;
    }

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        X_Tile = (int)Mathf.Floor(myCollider.bounds.center.x + offsetx);
        Y_Tile = (int)Mathf.Floor(myCollider.bounds.center.y + offsety);
    }
}
