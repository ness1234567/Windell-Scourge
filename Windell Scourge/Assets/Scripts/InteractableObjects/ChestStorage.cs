using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestStorage : MonoBehaviour, IContainer
{
    private static List<ChestStorage> chestList;
    private int _total_slots = 30;
    private int _occupied_slots = 0;
    private ItemStack[] _StorageSlots = new ItemStack[40];

    public bool addItemStack(int slot, ItemStack item)
    {
        //Check that slot is available and empty, if not don't do anything and return
        if (!(_StorageSlots[slot] == null)) { return false; }
        //add item to slot
        _StorageSlots[slot] = item;
        _occupied_slots++;
        return true;
    }

    public bool removeItemStack(int slot)
    {
        //Check that slot is available and actually as an item, if not don't do anything and return
        if (_StorageSlots[slot] == null) { return false; }

        //remove item from slot
        _StorageSlots[slot] = null;
        _occupied_slots--;

        return true;
    }

    public bool incrementSlot(int slot, int num)
    {
        //Check that slot has as an item, if not don't do anything and return
        if (_StorageSlots[slot] == null) { return false; }

        //Max stack limit
        int maxNum = _StorageSlots[slot].Item.maxStacks;

        //check if adding past stack limit
        if ((_StorageSlots[slot].quantity + num) > maxNum)
        {
            return false;
        }
        else
        {
            _StorageSlots[slot].quantity = _StorageSlots[slot].quantity + num;
        }
        return true;
    }

    public bool decrementSlot(int slot, int num)
    {
        //Check that slot has as an item, if not don't do anything and return
        if (_StorageSlots[slot] == null) { return false; }

        //check if adding past max stack limit
        if ((_StorageSlots[slot].quantity - num) < 0)
        {
            return false;
        }
        //check if it will remove whole stack
        else if (_StorageSlots[slot].quantity == num)
        {
            removeItemStack(slot);
        }
        else
        {
            _StorageSlots[slot].quantity = _StorageSlots[slot].quantity - num;
        }
        return true;
    }

    public ItemStack[] inventorySlots
    {
        get { return _StorageSlots; }
        set { _StorageSlots = value; }
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

    public ItemStack getItemStack(int slotNum)
    {
        if (_StorageSlots[slotNum] == null)
        {
            return null;
        }

        if (slotNum < _total_slots)
        {
            return _StorageSlots[slotNum];
        }
        else
        {
            return null;
        }
    }


    public Item getItem(int slotNum)
    {
        if (_StorageSlots[slotNum] == null)
        {
            return null;
        }

        if (slotNum < _total_slots)
        {
            return _StorageSlots[slotNum].Item;
        }
        else
        {
            return null;
        }
    }
}
