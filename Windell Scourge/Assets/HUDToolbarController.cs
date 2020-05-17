using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDToolbarController : MonoBehaviour
{
    [SerializeField]
    private InventoryObject InvObject;
    [SerializeField]
    private DraggedItemUI draggedItemUI;
    [SerializeField]
    private HUDToolbarUI ToolbarUI;

    void Awake()
    {
        /*//Subscribe handlers for hovering over item
        ToolbarUI.OnPointerEnterItemEvent += CursorEnterItem;
        ToolbarUI.OnPointerExitItemEvent += CursorExitItem;

        //Subscribe handler for a change in selected item
        ToolbarUI.selectedItemChange += ChangeSelectedItem;*/
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void CursorEnterItem(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1, 1, 1);
        Vector3 pos = i.Image.rectTransform.position;
        ItemHighlighUI highlight = this.GetComponentInChildren<ItemHighlighUI>();
        Debug.Log("cursor enter test");
        Debug.Log(pos);
        Debug.Log(highlight);
        //TODO
        //Calculate highlight image slot position
        /*float inv_initX = 9;
        float inv_initY = 50;

        int row = (int)(i.SlotID / 5);
        int col = i.SlotID % 5;
        float x = inv_initX + (col * 20);
        float y = inv_initY - (row * 18);*/

        highlight.activateHighlight(pos.x, pos.y, 1.25f, 1.25f);
    }

    private void CursorExitItem(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1, 1, 1);
        ItemHighlighUI highlight = this.GetComponentInChildren<ItemHighlighUI>();
        highlight.deactivateHighlight(i.SlotID);
    }

    private void ItemClick(SlotUI i)
    {
        if ((i.item != null) && (draggedItemUI.item != null))
        {
            //SwapItem(i);
        }
        else if ((i.item != null) && (draggedItemUI.item == null))
        {
            //SelectItem(i);
        }
        else if ((i.item == null) && (draggedItemUI.item != null))
        {
            //InsertItem(i);
        }
    }

    private void ChangeSelectedItem(int slotID)
    {
        Debug.Log("Changing selected Item!");
        InvObject.selectedItemID = slotID + 30;
    }
}
