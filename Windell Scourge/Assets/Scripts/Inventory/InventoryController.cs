using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

//Handles the inventory logic and what to do with user input
public class InventoryController : MonoBehaviour
{
    private static InventoryController _instance;
    [SerializeField]
    private InventoryUI InvUI;
    [SerializeField]
    private ItemHighlighUI highlight;
    [SerializeField]
    private InventoryObject InvObject;
    [SerializeField]
    private DraggedItemUI draggedItemUI;
    [SerializeField]
    private HUDToolbarUI ToolbarUI;
    [SerializeField]
    SelectionOutlineUI selectionOutline;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        //Subscribe handlers for clicking on item
        InvUI.OnLeftClickItemEvent += main_ItemClick;

        //Subscribe handlers for hovering over item
        InvUI.OnPointerEnterItemEvent += main_CursorEnterItem;
        InvUI.OnPointerExitItemEvent += main_CursorExitItem;

        //Subscribe handlers for hovering over item
        ToolbarUI.OnPointerEnterItemEvent += toolbar_CursorEnterItem;
        ToolbarUI.OnPointerExitItemEvent += toolbar_CursorExitItem;

        //Subscribe handler for a change in selected item
        ToolbarUI.OnLeftClickItemEvent += toolbar_ItemClick;

        InvUI.RefreshInventoryUI();
    }

    public static InventoryController Instance { get { return _instance; } }

    private void main_CursorEnterItem(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1.125f, 1.125f, 1);
        highlight = InvUI.GetComponentInChildren<ItemHighlighUI>();
        //move highlight image to slot position
        if ((i.SlotID >= 0) && (i.SlotID <= 29))
        {
            float inv_initX = 9;
            float inv_initY = 50;

            int row = (int)(i.SlotID / 5);
            int col = i.SlotID % 5;

            float x = inv_initX + (col * 20);
            float y = inv_initY - (row * 18);

            highlight.activateHighlight(x, y, 1, 1);
        }
        else if ((i.SlotID >= 30) && (i.SlotID <= 39))
        {
            float tool_initX = -91;
            float tool_initY = -69;

            float x = tool_initX + (20 * (i.SlotID - 30));

            highlight.activateHighlight(x, tool_initY, 1, 1);
        }
    }

    private void main_CursorExitItem(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1, 1, 1);
        highlight = InvUI.GetComponentInChildren<ItemHighlighUI>();
        highlight.deactivateHighlight();
    }

    private void main_ItemClick(SlotUI i)
    {
        if ((InvObject.getItem(i.SlotID) != null) && (draggedItemUI.item != null))
        {
            //If same item, stack them
            if(InvObject.getItem(i.SlotID).itemData.item_id == draggedItemUI.item.itemData.item_id)
            {
                StackItem(i);
            }
            //If not same item, then swap positions
            else
            {
                SwapItem(i);
            }
        }
        else if ((InvObject.getItem(i.SlotID) != null) && (draggedItemUI.item == null))
        {
            SelectItem(i);
        }
        else if ((InvObject.getItem(i.SlotID) == null) && (draggedItemUI.item != null))
        {
            InsertItem(i);
        }
    }

    private void SwapItem(SlotUI i)
    {
        ItemStack temp = draggedItemUI.item;
        draggedItemUI.item = InvObject.getItem(i.SlotID);
        InvObject.addItem(i.SlotID, temp);
        i.Image.rectTransform.localScale = new Vector3(1.125f, 1.125f, 1);
        InvUI.RefreshInventoryUI();
    }

    private void StackItem(SlotUI i)
    {
        int maxStackNum = draggedItemUI.item.itemData.maxStacks;
        int numA = InvObject.getItem(i.SlotID).quantity;
        int numB = draggedItemUI.item.quantity;
        int total = numA + numB;
        if (total > maxStackNum)
        {
            int diff = total - maxStackNum;
            draggedItemUI.item.quantity = diff;
            InvObject.getItem(i.SlotID).quantity = maxStackNum;
        } else
        {
            InvObject.getItem(i.SlotID).quantity = total;
            Destroy(draggedItemUI.item);
            draggedItemUI.item = null;
        }
    }

    private void SelectItem(SlotUI i)
    {
        draggedItemUI.item = InvObject.getItem(i.SlotID);
        InvObject.removeItem(i.SlotID);
        InvUI.RefreshInventoryUI();
    }

    private void InsertItem(SlotUI i)
    {
        InvObject.addItem(i.SlotID, draggedItemUI.item);
        InvUI.RefreshInventoryUI();
        i.Image.rectTransform.localScale = new Vector3(1.125f, 1.125f, 1);
        draggedItemUI.item = null;
    }

    //////////////////////////////////////////////////////////////////

    private void toolbar_CursorEnterItem(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1.125f, 1.125f, 1);

        if ((i.SlotID >= 30) && (i.SlotID <= 39))
        {
            float tool_initX = -90;
            float tool_initY = -2;

            float x = tool_initX + (20 * (i.SlotID - 30));
            highlight = ToolbarUI.GetComponentInChildren<ItemHighlighUI>();
            highlight.activateHighlight(x, tool_initY, 1, 1);
        }
    }

    private void toolbar_CursorExitItem(SlotUI i)
    {
        if (i.SlotID == InvObject.selectedItemID)
        {
            i.Image.rectTransform.localScale = new Vector3(1.125f, 1.125f, 1);
        }
        else
        {
            i.Image.rectTransform.localScale = new Vector3(1, 1, 1);
        }
        highlight = ToolbarUI.GetComponentInChildren<ItemHighlighUI>();
        highlight.deactivateHighlight();
    }

    private void toolbar_ItemClick(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1.125f, 1.125f, 1);

        //update mode
        InvObject.selectedItemID = i.SlotID;

        //Update UI current tool outline position
        selectionOutline.updatePos(i.SlotID - 30);

        //refresh toolbar
        ToolbarUI.RefreshToolbarUI();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    public int AutoStackItem(ItemData item, int num)
    {
        //TODO
        int numLeft = num;
        int maxStackNum = item.maxStacks;

        //Loop through toolbar and main inventory to find stack 
        if (item.stackable == true)
        {
            for (int i = 30; i < 40; i++)
            {
                if ((InvObject.getItem(i).itemData.item_id == item.item_id) && (InvObject.getItem(i).quantity != maxStackNum))
                {
                    int total = InvObject.getItem(i).quantity + numLeft;
                    if (total > maxStackNum)
                    {
                        numLeft = total - maxStackNum;
                        InvObject.getItem(i).quantity = maxStackNum;
                    }
                    else
                    {
                        numLeft = 0;
                        break;
                    }
                }
            }

            for (int i = 0; i < 30; i++)
            {
                if (numLeft == 0)
                    break;

                if ((InvObject.getItem(i).itemData.item_id == item.item_id) && (InvObject.getItem(i).quantity != maxStackNum))
                {
                    int total = InvObject.getItem(i).quantity + numLeft;
                    if (total > maxStackNum)
                    {
                        numLeft = total - maxStackNum;
                        InvObject.getItem(i).quantity = maxStackNum;
                    }
                    else
                    {
                        numLeft = 0;
                        break;
                    }
                }
            }
        }

        //Check if inventory is full
        if (InvObject.occupiedSlots == InvObject.totalSlots)
        {
            return numLeft;
        }

        //TODO
        GameObject obj = new GameObject("empty");
        ItemStack stack = obj.AddComponent<ItemStack>();
        stack.itemData = item;
        stack.quantity = numLeft;

        //loop through toolbar to find empty slot
        for (int i = 30; i < 40; i++)
        {
            if (numLeft == 0)
                break;

            if (InvObject.getItem(i) == null)
            {
                InvObject.addItem(i, stack);
                numLeft = 0;
            }
        }

        //loop through main inventory to find empty slot
        for (int i = 0; i < 30; i++)
        {
            if (numLeft == 0)
                break;

            if (InvObject.getItem(i) == null)
            {
                InvObject.addItem(i, stack);
                numLeft = 0;
            }
        }

        InvUI.RefreshInventoryUI();
        ToolbarUI.RefreshToolbarUI();

        return numLeft;
    }

}
