using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Hoe", menuName = "Item/Hoe")]
public class Hoe : Item
{
    [SerializeField]
    private int _ToolTier;

    public override void charge()
    {
        Debug.Log("charging Hoe!");


    }

    public override void use() {

        playerController player = playerController.Instance;

        //int numframes = _ToolTier;
        int numframes = 3;          //<--- Temporary for testing
        float time = player.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        int currframe = (int)(time*numframes + 1);
        Debug.Log("Using Hoe at Level " + currframe + " !");

        //Determine tiles to plough
        int x1, x2, y1, y2;
        x1 = x2 = y1 = y2 = 0;

        int playerDir = player.Animator.GetInteger("CurrentDir");
        Debug.Log("IN ITEM FUNC: currentDir = " + playerDir);

        Vector3Int playerPosCell = TileMapManager.Instance.tilemap.WorldToCell(player.transform.position);

        if (playerDir == 0)
        {
            x1 = x2 = playerPosCell.x;
            y1 = y2 = playerPosCell.y - 1;
        }
        else if (playerDir == 1)
        {
            x1 = x2 = playerPosCell.x - 1;
            y1 = y2 = playerPosCell.y;
        }
        else if (playerDir == 2)
        {
            x1 = x2 = playerPosCell.x;
            y1 = y2 = playerPosCell.y + 1;
        }
        else if (playerDir == 3)
        {
            x1 = x2 = playerPosCell.x + 1;
            y1 = y2 = playerPosCell.y;
        }

        /*if (currframe == 1)
        {
            if (playerDir == 0)
            {
                x1 = x2 = playerPosCell.x;
                y1 = y2 = playerPosCell.y - 1;
            }
            else if (playerDir == 1)
            {
                x1 = x2 = playerPosCell.x - 1;
                y1 = y2 = playerPosCell.y;
            }
            else if (playerDir == 2)
            {
                x1 = x2 = playerPosCell.x;
                y1 = y2 = playerPosCell.y + 1;
            }
            else if (playerDir == 3)
            {
                x1 = x2 = playerPosCell.x + 1;
                y1 = y2 = playerPosCell.y;
            }
        }
        else if (currframe == 2)
        {

        }
        else if (currframe == 3)
        {

        }
        else if (currframe == 4)
        {

        }*/
        Debug.Log("playerPos = " + playerPosCell.x + ", " + playerPosCell.y);
        TileMapManager.Instance.ploughTiles(x1, x2, y1, y2);

    }

}
