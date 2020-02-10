using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InventoryObject : MonoBehaviour
{
    private int _total_slots = 30;
    private int _occupied_slots = 0;
    private ItemObject[] _InventorySlots = new ItemObject[30];

    // Start is called before the first frame update
    void Start()
    {
        //Initialise inventory as empty
        //for (int i = 0; i < _total_slots; i++) _InventorySlots[i] = null;
        //Debug.Log("Adding hoe to slot 1");
        ItemObject hoe = (ItemObject)AssetDatabase.LoadAssetAtPath("Assets/Items/Copper_Hoe.asset", typeof(ItemObject));
        addItem(0, hoe);
        //Debug.Log("Slot 1 is now:");
        //Debug.Log(getItem(0));
    }

    public bool addItem(int slot, ItemObject item)
    {
        //Check that slot is available and empty, if not don't do anything and return
        if (!(_InventorySlots[slot] == null)) { return false; }

        //add item to slot
        _InventorySlots[slot] = item;
        _occupied_slots++;
        return true;
    }

    public bool removeItem(int slot, ItemObject item)
    {
        //Check that slot is available and actually as an item, if not don't do anything and return
        if (_InventorySlots[slot] == null) { return false; }

        //remove item from slot
        _InventorySlots[slot] = null;
        _occupied_slots--;
        return true;
    }

    public ItemObject[] inventorySlots
    {
        get { return _InventorySlots; }
        set { _InventorySlots = value; }
    }

    public int totalSlots
    {
        get { return _total_slots; }
        set { _total_slots = value; }
    }

    public int occupiedSlots
    {
        get { return _occupied_slots; }
        set { _occupied_slots = value; }
    }

    public ItemObject getItem(int slotNum)
    {
        if (slotNum < _total_slots)
        {
            return _InventorySlots[slotNum];
        } else
        {
            return null;
        }
    }
}
