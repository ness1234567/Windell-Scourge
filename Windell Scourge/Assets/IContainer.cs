using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContainer
{
    bool addItemStack(int slot, ItemStack item);
    bool removeItemStack(int slot);

    bool incrementSlot(int slot, int num);
    bool decrementSlot(int slot, int num);

    int totalSlots { get; set; }
    int occupiedSlots { get; set; }

    ItemStack getItemStack(int slotNum);
    Item getItem(int slotNum);

}
