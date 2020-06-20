using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Contains the inventory data structure and basic manipulations of the data structure
public class InventoryObject : MonoBehaviour
{
    private static InventoryObject _instance;
    private int _total_slots = 40;
    private int _occupied_slots = 0;
    private ItemStack[] _InventorySlots = new ItemStack[40];

    private int _selectedItemID = 30;

    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
    }

    public static InventoryObject Instance { get { return _instance; } }

    public bool addItem(int slot, ItemStack item)
    {
        //Check that slot is available and empty, if not don't do anything and return
        if (!(_InventorySlots[slot] == null)) { return false; }

        //add item to slot
        _InventorySlots[slot] = item;
        _occupied_slots++;
        return true;
    }

    public bool removeItem(int slot)
    {
        //Check that slot is available and actually as an item, if not don't do anything and return
        if (_InventorySlots[slot] == null) { return false; }

        //remove item from slot
        _InventorySlots[slot] = null;
        _occupied_slots--;

        return true;
    }

    public bool incrementSlot(int slot, int num)
    {
        //Check that slot has as an item, if not don't do anything and return
        if (_InventorySlots[slot] == null) { return false; }

        //TODO
        //increase quantity of item in inventory by num

        return true;
    }

    public bool decrementSlot(int slot, int num)
    {
        //Check that slot has as an item, if not don't do anything and return
        if (_InventorySlots[slot] == null) { return false; }

        //TODO
        //decrease quantity of item in inventory by num

        return true;
    }
    public ItemStack[] inventorySlots
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

    public int selectedItemID
    {
        get { return _selectedItemID; }
        set { _selectedItemID = value; }
    }

    public ItemStack getSelectedItem()
    {
        return _InventorySlots[_selectedItemID];
    }


    public ItemStack getItem(int slotNum)
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
