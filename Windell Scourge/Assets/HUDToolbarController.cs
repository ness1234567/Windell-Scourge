using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDToolbarController : MonoBehaviour
{
    [SerializeField]
    private InventoryObject InvObject;
    [SerializeField]
    private HUDToolbarUI ToolbarUI;
    [SerializeField]
    SelectionOutlineUI selectionOutline;

    void Awake()
    {
        //Subscribe handlers for hovering over item
        ToolbarUI.OnPointerEnterItemEvent += CursorEnterItem;
        ToolbarUI.OnPointerExitItemEvent += CursorExitItem;

        //Subscribe handler for a change in selected item
        ToolbarUI.OnLeftClickItemEvent += ItemClick;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void CursorEnterItem(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1.125f, 1.125f, 1);
        ItemHighlighUI highlight = this.GetComponentInChildren<ItemHighlighUI>();

        if ((i.SlotID >= 30) && (i.SlotID <= 39))
        {
            float tool_initX = -90;
            float tool_initY = -2;

            float x = tool_initX + (20 * (i.SlotID - 30));

            highlight.activateHighlight(x, tool_initY, 1, 1);
        }
    }

    private void CursorExitItem(SlotUI i)
    {
        if (i.SlotID == InvObject.selectedItemID)
        {
            i.Image.rectTransform.localScale = new Vector3(1.125f, 1.125f, 1);
        }
        else
        {
            i.Image.rectTransform.localScale = new Vector3(1, 1, 1);
        }
        ItemHighlighUI highlight = this.GetComponentInChildren<ItemHighlighUI>();
        highlight.deactivateHighlight(i.SlotID);
    }

    private void ItemClick(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1.125f, 1.125f, 1);

        //update mode
        InvObject.selectedItemID = i.SlotID;

        //Update UI current tool outline position
        selectionOutline.updatePos(i.SlotID-30);

        //refresh toolbar
        ToolbarUI.RefreshToolbarUI();
    }
}
