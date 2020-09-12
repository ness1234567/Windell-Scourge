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
    private InventoryObject InvObject;
    [SerializeField]
    private DraggedItemUI draggedItemUI;
    [SerializeField]
    private SelectionOutlineUI selectionOutline;
    [SerializeField]
    public GameObject DroppedItemPrefab;

    /////////////////////////// UI INTERACTABLE ITEM CONTAINERS //////////////////////////////////

    [SerializeField]
    private InventoryUI InvUI;
    [SerializeField]
    private HUDToolbarUI ToolbarUI;
    [SerializeField]
    private ChestUI chestUI;

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

        //////// INVENTORY MAIN ///////////////////

        //Subscribe handlers for clicking on item
        InvUI.OnLeftClickItemEvent += container_ItemClick;

        //Subscribe handler for clicking outside Inventory
        InvUI.OnPointerClickOutside += DropItem;

        //////////// TOOLBAR ///////////////////////

        //Subscribe handler for a change in selected item
        ToolbarUI.OnLeftClickItemEvent += toolbar_ItemClick;

        /////////////// CHEST //////////////////////////////

        //Subscribe handlers for clicking on item
        chestUI.OnLeftClickItemEvent += container_ItemClick;

        //Subscribe handler for clicking outside chest UI
        chestUI.OnPointerClickOutside += DropItem;

    }

    public static InventoryController Instance { get { return _instance; } }
    public InventoryObject Inventory { get { return InvObject; } }

    ////////////////////////////// MAIN INVENTORY CALLBACKS ////////////////////////////////////

    private void container_ItemClick(SlotUI i, IContainer container, IUserInterface UI)
    {
        Debug.Log("item click: " + i.SlotID);
        Debug.Log(container.getItem(i.SlotID));
        Debug.Log((draggedItemUI.itemStack != null));

        if ((container.getItem(i.SlotID) != null) && (draggedItemUI.itemStack != null))
        {
            Debug.Log("A");

            //If same item, stack them
            if (container.getItem(i.SlotID).item_id == draggedItemUI.Item.item_id)
            {
                StackItem(i, container);
            }
            //If not same item, then swap positions
            else
            {
                SwapItem(i, container);
            }
        }
        else if ((container.getItem(i.SlotID) != null) && (draggedItemUI.itemStack == null))
        {
            Debug.Log("B");

            SelectItem(i, container);
        }
        else if ((container.getItem(i.SlotID) == null) && (draggedItemUI.itemStack != null))
        {
            Debug.Log("C");

            InsertItem(i, container);
        }
        UI.Refresh();
    }

    private void SwapItem(SlotUI i, IContainer container)
    {
        ItemStack temp = draggedItemUI.itemStack;
        draggedItemUI.itemStack = container.getItemStack(i.SlotID);
        container.addItemStack(i.SlotID, temp);
        i.Image.rectTransform.localScale = new Vector3(1.25f, 1.25f, 1);
    }

    private void StackItem(SlotUI i, IContainer container)
    {
        int maxStackNum = draggedItemUI.Item.maxStacks;
        int numA = container.getItemStack(i.SlotID).quantity;
        int numB = draggedItemUI.itemStack.quantity;
        int total = numA + numB;
        if (total > maxStackNum)
        {
            int diff = total - maxStackNum;
            draggedItemUI.itemStack.quantity = diff;
            container.getItemStack(i.SlotID).quantity = maxStackNum;
        } else
        {
            container.getItemStack(i.SlotID).quantity = total;
            //Destroy(draggedItemUI.itemStack);
            draggedItemUI.itemStack = null;
        }
    }

    private void SelectItem(SlotUI i, IContainer container)
    {
        Debug.Log("item pick: " + i.SlotID);
        draggedItemUI.itemStack = container.getItemStack(i.SlotID);
        container.removeItemStack(i.SlotID);
    }

    private void InsertItem(SlotUI i, IContainer container)
    {
        container.addItemStack(i.SlotID, draggedItemUI.itemStack);
        i.Image.rectTransform.localScale = new Vector3(1.25f, 1.25f, 1);
        draggedItemUI.itemStack = null;
    }

    private void DropItem()
    {
        if (draggedItemUI.itemStack != null)
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

    ////////////////////////////// CHEST STORAGE CALLBACKS ////////////////////////////////////


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

    public int getCurrentItemID()
    {
        if (InvObject.getSelectedItem() != null)
            return InvObject.selectedItemID;
        else
            return -1;
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
        InvUI.Refresh();
        ToolbarUI.RefreshToolbarUI();
        return numLeft;
    }

}
