using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap tm;
    [SerializeField]
    private TileBase[] ploughedTile;
    [SerializeField]
    private TileBase dirtTile;


    private static TileMapManager _instance;
    public static TileMapManager Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Tilemap tilemap
    {
        get { return tm; }
        set { tm = value; }
    }

    public void ploughTiles(int x1, int x2, int y1, int y2)
    {
        int playerDir = playerController.Instance.Animator.GetInteger("CurrentDir");
        Debug.Log("IN TILE FUNC: currentDir = " + playerDir);
        //Loop through all affected tile twice. 1st to determine which tiles are ploughed within 2 tiles around the hit area. 2nd to detemine which ploughed sprite to use.
        int cols = y2 - y1 + 5;
        int rows = x2 - x1 + 5;
        int[,] area = new int[rows, cols];
        Vector3Int currTile = new Vector3Int(0, 0, 0);

        //1st RUN: detemine which tiles are ploughed and need to change sprite
        int i, j;       //Maxtrix X,Y indices
        i = 0;  
        for (int tx = (x1-2); tx <= (x2+2); tx++)
        {
            j = 0;
            for (int ty = (y1-2); ty <= (y2+2); ty++)
            {
                currTile.Set(tx, ty, 0);

                //Tile ploughed by hoe

                if ((tx >= x1) && (tx <= x2) && (ty >= y1) && (ty <= y2))
                {
                    if (tm.GetTile(currTile).name.Contains("ploughed") || tm.GetTile(currTile) == dirtTile)
                        area[i, j] = 1;
                    else
                        area[i, j] = 0;
                } else
                //Tile surrounding the tiles ploughed by hoe
                {
                    if  (tm.GetTile(currTile).name.Contains("ploughed"))
                        area[i, j] = 1;
                    else
                        area[i, j] = 0;
                }
                if (area[i, j] == 1)
                {
                    Debug.Log("area[" + i + "," + j + "] =" + area[i, j]);
                    Debug.Log("at position: " + tx + ", " + ty);
                }     
                //Determine if object is on tile. If so, set to area[i,j] = 0
                //TODO

                j = j + 1;
            }
            i = i + 1;
        }

        //2nd RUN: determine what sprites to use for ploughed tiles
        int up, down, left, right;
        up = down = left = right = 0;

        //find number of ploughed tiles in 2d array
        int numPloughedTiles = 0;
        for (i = 1; i <= cols - 2; i++)
        {
            for (j = 1; j <= rows - 2; j++)
            {
                numPloughedTiles = numPloughedTiles + area[i, j];
            }
        }
        Debug.Log("num plough = " + numPloughedTiles);

        Vector3Int[] positions = new Vector3Int[numPloughedTiles];
        TileBase[] tileArray = new TileBase[positions.Length];
        Debug.Log("second RUN");
        int Index = 0;
        for (i = 1; i <= cols-2; i++)
        {
            for(j = 1; j <= rows-2; j++)
            {
                if (area[i,j] == 1)
                {
                    int tx = x1 + i - 2;
                    int ty = y1 + j - 2;

                    currTile.Set(tx, ty, 0);
                    positions[Index] = currTile;
                    
                    up = area[i, j + 1];
                    down = area[i, j - 1];
                    left = area[i - 1, j];
                    right = area[i + 1, j];

                    TileBase tile = determinePloughTile(up, down, left, right);
                    tileArray[Index] = tile;
                    Debug.Log("area[" + i + "," + j + "] =" + area[i, j]);
                    Debug.Log("Setting tile at " + tx + ", " + ty + " to " + tile.name);
                    Index = Index + 1;
                }
            }
        }
        tm.SetTiles(positions, tileArray);
    }

    TileBase determinePloughTile(int up, int down, int left, int right)
    {
        TileBase tile;
        if((up == 1) && (down == 1) && (left == 1) && (right == 1))
        {
            tile = ploughedTile[0];
        }
        else if ((up == 1) && (down == 1) && (right == 1))
        {
            tile = ploughedTile[1];
        }
        else if ((down == 1) && (left == 1) && (right == 1))
        {
            tile = ploughedTile[2];
        }
        else if ((up == 1) && (down == 1) && (left == 1))
        {
            tile = ploughedTile[3];
        }
        else if ((up == 1) && (left == 1) && (right == 1))
        {
            tile = ploughedTile[4];
        }
        else if ((up == 1) && (right == 1))
        {
            tile = ploughedTile[5];
        }
        else if ((down == 1) && (right == 1))
        {
            tile = ploughedTile[6];
        }
        else if ((down == 1) && (left == 1))
        {
            tile = ploughedTile[7];
        }
        else if ((up == 1) && (left == 1))
        {
            tile = ploughedTile[8];
        }
        else if ((up == 1) && (down == 1))
        {
            tile = ploughedTile[9];
        }
        else if ((left == 1) && (right == 1))
        {
            tile = ploughedTile[10];
        }
        else if (up == 1) 
        {
            tile = ploughedTile[11];
        }
        else if (down == 1)
        {
            tile = ploughedTile[12];
        }
        else if (left == 1)
        {
            tile = ploughedTile[13];
        }
        else if (right == 1)
        {
            tile = ploughedTile[14];
        } else
        {
            tile = ploughedTile[15];
        }

        return tile;
    }

}
