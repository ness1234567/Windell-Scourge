using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Contains the inventory data structure and basic manipulations of the data structure
public class InventoryObject : MonoBehaviour, IContainer
{
    private int _total_slots = 40;
    private int _occupied_slots = 0;
    private ItemStack[] _InventorySlots = new ItemStack[40];

    private int _selectedItemID = 30;

    public bool addItemStack(int slot, ItemStack item)
    {
        //Check that slot is available and empty, if not don't do anything and return
        if (!(_InventorySlots[slot] == null)) { return false; }
        //add item to slot
        _InventorySlots[slot] = item;
        _occupied_slots++;
        return true;
    }

    public bool removeItemStack(int slot)
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

        //Max stack limit
        int maxNum = _InventorySlots[slot].Item.maxStacks;

        //check if adding past stack limit
        if ((_InventorySlots[slot].quantity+num) > maxNum)
        {
            return false;
        }
        else
        {
            _InventorySlots[slot].quantity = _InventorySlots[slot].quantity + num;
        }
        return true;
    }

    public bool decrementSlot(int slot, int num)
    {
        //Check that slot has as an item, if not don't do anything and return
        if (_InventorySlots[slot] == null) { return false; }

        //check if adding past max stack limit
        if ((_InventorySlots[slot].quantity-num) < 0)
        {
            return false;
        }
        //check if it will remove whole stack
        else if (_InventorySlots[slot].quantity == num)
        {
            removeItemStack(slot);
        }
        else
        {
            _InventorySlots[slot].quantity = _InventorySlots[slot].quantity - num;
        }
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

    public ItemStack getItemStack(int slotNum)
    {
        if(_InventorySlots[slotNum] == null)
        {
            return null;
        }

        if (slotNum < _total_slots)
        {
            return _InventorySlots[slotNum];
        }
        else
        {
            return null;
        }
    }


    public Item getItem(int slotNum)
    {
        if (_InventorySlots[slotNum] == null)
        {
            return null;
        }

        if (slotNum < _total_slots)
        {
            return _InventorySlots[slotNum].Item;
        } else
        {
            return null;
        }
    }
}
