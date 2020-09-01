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
    private SelectionOutlineUI selectionOutline;
    [SerializeField]
    public GameObject DroppedItemPrefab;

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

        //Subscribe handler for clicking outside Inventory
        InvUI.OnPointerClickOutside += DropItem;

        //Subscribe handlers for hovering over item
        ToolbarUI.OnPointerEnterItemEvent += toolbar_CursorEnterItem;
        ToolbarUI.OnPointerExitItemEvent += toolbar_CursorExitItem;

        //Subscribe handler for a change in selected item
        ToolbarUI.OnLeftClickItemEvent += toolbar_ItemClick;
    }

    public static InventoryController Instance { get { return _instance; } }
    public InventoryObject Inventory { get { return InvObject; } }

    private void main_CursorEnterItem(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1.25f, 1.25f, 1);
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
        if ((InvObject.getItem(i.SlotID) != null) && (draggedItemUI.itemStack != null))
        {
            //If same item, stack them
            if(InvObject.getItem(i.SlotID).item_id == draggedItemUI.Item.item_id)
            {
                StackItem(i);
            }
            //If not same item, then swap positions
            else
            {
                SwapItem(i);
            }
        }
        else if ((InvObject.getItem(i.SlotID) != null) && (draggedItemUI.itemStack == null))
        {
            SelectItem(i);
        }
        else if ((InvObject.getItem(i.SlotID) == null) && (draggedItemUI.itemStack != null))
        {
            InsertItem(i);
        }
    }

    private void SwapItem(SlotUI i)
    {
        ItemStack temp = draggedItemUI.itemStack;
        draggedItemUI.itemStack = InvObject.getItemStack(i.SlotID);
        InvObject.addItemStack(i.SlotID, temp);
        i.Image.rectTransform.localScale = new Vector3(1.25f, 1.25f, 1);
        InvUI.RefreshInventoryUI();
    }

    private void StackItem(SlotUI i)
    {
        int maxStackNum = draggedItemUI.Item.maxStacks;
        int numA = InvObject.getItemStack(i.SlotID).quantity;
        int numB = draggedItemUI.itemStack.quantity;
        int total = numA + numB;
        if (total > maxStackNum)
        {
            int diff = total - maxStackNum;
            draggedItemUI.itemStack.quantity = diff;
            InvObject.getItemStack(i.SlotID).quantity = maxStackNum;
        } else
        {
            InvObject.getItemStack(i.SlotID).quantity = total;
            //Destroy(draggedItemUI.itemStack);
            draggedItemUI.itemStack = null;
        }
    }

    private void SelectItem(SlotUI i)
    {
        draggedItemUI.itemStack = InvObject.getItemStack(i.SlotID);
        InvObject.removeItemStack(i.SlotID);
        InvUI.RefreshInventoryUI();
    }

    private void InsertItem(SlotUI i)
    {
        InvObject.addItemStack(i.SlotID, draggedItemUI.itemStack);
        InvUI.RefreshInventoryUI();
        i.Image.rectTransform.localScale = new Vector3(1.25f, 1.25f, 1);
        draggedItemUI.itemStack = null;
    }

    private void DropItem()
    {
        if (draggedItemUI.Item != null)
        {
            //Create new droppedItem prefab
            Transform playerTransform = playerController.Instance.transform;
            Vector3 spawnLocation = new Vector3(playerTransform.localPosition.x, playerTransform.localPosition.y - 1, playerTransform.localPosition.z);
            GameObject obj = Instantiate(DroppedItemPrefab, spawnLocation, Quaternion.identity);

            //initialise values of new object
            DroppedItem droppedItem = obj.GetComponent<DroppedItem>();
            droppedItem.Item = draggedItemUI.Item;
            droppedItem.Qauntity = draggedItemUI.itemStack.quantity;

            //Remove item from inventory/dragdropUI
            draggedItemUI.itemStack = null;
        }
    }

    ////////////////////////////// TOOLBAR CALLBACKS ////////////////////////////////////

    private void toolbar_CursorEnterItem(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1.25f, 1.25f, 1);

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
            i.Image.rectTransform.localScale = new Vector3(1.25f, 1.25f, 1);
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
        i.Image.rectTransform.localScale = new Vector3(1.25f, 1.25f, 1);

        //update mode
        InvObject.selectedItemID = i.SlotID;

        //Update UI current tool outline position
        selectionOutline.updatePos(i.SlotID - 30);

        //refresh toolbar
        ToolbarUI.RefreshToolbarUI();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    public void useItem()
    {
        ItemStack currentItemStack = InvObject.getSelectedItem();

        //check if selected hotbar has an item
        if (currentItemStack == null)
            return;

        //check if not dragging item. If dragging item, it should drop item instead
        if (draggedItemUI.itemStack != null)
            return;
            
        //check if clicking on UI
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        //Use item
        currentItemStack.Item.use();

        //decrement item if consumable
        if (currentItemStack.Item.consumable)
        {
            //consume item if consumable
            currentItemStack.quantity = currentItemStack.quantity - 1;

            //if consumed last item in stack, delete from inventory
            if(currentItemStack.quantity == 0)
            {
                InvObject.removeItemStack(InvObject.selectedItemID);
            } else
            {
                InvObject.decrementSlot(InvObject.selectedItemID, 1);
            }
        }
    }

    public void chargeItem()
    {
        ItemStack currentItemStack = InvObject.getSelectedItem();

        //check if selected hotbar has an item
        if (currentItemStack == null)
            return;

        //check if not dragging item. If dragging item, it should drop item instead
        if (draggedItemUI.itemStack != null)
            return;

        //check if clicking on UI
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        //charge item
        currentItemStack.Item.charge();
    }
    
    public Item getCurrentItem()
    {
        if (InvObject.getSelectedItem() != null)
            return InvObject.getSelectedItem().Item;
        else
            return null;
    }

    public int AutoStackItem(Item pickedItem, int num)
    {
        //TODO
        int numLeft = num;
        int maxStackNum = pickedItem.maxStacks;

        //Loop through toolbar and main inventory to find stack 
        if (pickedItem.stackable == true)
        {
            //if found same item in toolbar and not at max stack, stack the new item
            for (int i = 30; i < 40; i++)
            {
                if ((InvObject.getItem(i).item_id == pickedItem.item_id) && (InvObject.getItemStack(i).quantity != maxStackNum))
                {
                    int total = InvObject.getItemStack(i).quantity + numLeft;
                    if (total > maxStackNum)
                    {
                        numLeft = total - maxStackNum;
                        InvObject.getItemStack(i).quantity = maxStackNum;
                    }
                    else
                    {
                        numLeft = 0;
                        break;
                    }
                }
            }

            //if found same item in Inventory and not at max stack, stack the new item
            for (int i = 0; i < 30; i++)
            {
                if (numLeft == 0)
                    break;

                if ((InvObject.getItem(i).item_id == pickedItem.item_id) && (InvObject.getItemStack(i).quantity != maxStackNum))
                {
                    int total = InvObject.getItemStack(i).quantity + numLeft;
                    if (total > maxStackNum)
                    {
                        numLeft = total - maxStackNum;
                        InvObject.getItemStack(i).quantity = maxStackNum;
                    }
                    else
                    {
                        numLeft = 0;
                        break;
                    }
                }
            }
        }

        //MAKE NEW STACK OF ITEM
        if ((InvObject.occupiedSlots != InvObject.totalSlots) && (numLeft != 0))
        {   

            //TODO
            //If inventory is not full, make a new stack of item and insert into inventory
            ItemStack stack = new ItemStack();
            //spawn item from database                                                                                                  
            stack.Item = pickedItem;
            stack.quantity = numLeft;

            //obj.transform.parent = this.transform;

            //loop through toolbar to find empty slot
            for (int i = 30; i < 40; i++)
            {
                if (numLeft == 0)
                    break;

                if (InvObject.getItem(i) == null)
                {
                    bool test = InvObject.addItemStack(i, stack);
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
                    InvObject.addItemStack(i, stack);
                    numLeft = 0;
                }
            }
        }
        InvUI.RefreshInventoryUI();
        ToolbarUI.RefreshToolbarUI();
        return numLeft;
    }

}
