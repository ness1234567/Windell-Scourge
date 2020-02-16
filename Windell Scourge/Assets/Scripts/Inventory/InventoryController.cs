using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private InventoryUI InvUI;
    [SerializeField]
    private InventoryObject InvObject;
    [SerializeField]
    private DraggedItemUI SelectedItemUI;

    //temp!! REMOVE LATER!
    public ItemObject tempHoe;

    void Awake()
    {
        //Initialise inventory as empty 
        InvObject.addItem(0, tempHoe);

        //Subscribe handlers for each different event relating to inventory
        InvUI.OnLeftClickItemEvent += ItemClick;
        InvUI.OnPointerEnterItemEvent += CursorEnterItem;
        InvUI.OnPointerExitItemEvent += CursorExitItem;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void CursorEnterItem(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1.125f, 1.125f, 1);
    }

    private void CursorExitItem(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1, 1, 1);
    }

    private void ItemClick(SlotUI i)
    {
        if ((i.item != null) && (SelectedItemUI.DraggedItem != null))
        {
            SwapItem(i);
        }
        else if ((i.item != null) && (SelectedItemUI.DraggedItem == null))
        {
            SelectItem(i);
        }
        else if ((i.item == null) && (SelectedItemUI.DraggedItem != null))
        {
            InsertItem(i);
        }
        InvUI.RefreshInventoryUI();
    }

    private void SwapItem(SlotUI i)
    {
        ItemObject temp = SelectedItemUI.DraggedItem;
        SelectedItemUI.DraggedItem = i.item;
        InvObject.addItem(i.SlotID, temp);
    }

    private void SelectItem(SlotUI i)
    {
        SelectedItemUI.DraggedItem = i.item;
        InvObject.removeItem(i.SlotID);
    }

    private void InsertItem(SlotUI i)
    {
        InvObject.addItem(i.SlotID, SelectedItemUI.DraggedItem);
        SelectedItemUI.DraggedItem = null;
    }
}
