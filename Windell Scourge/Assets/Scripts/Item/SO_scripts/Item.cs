using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum itemType
{
    Hoe = 0,
    Pickaxe = 1,
    watering_can = 2,
    food = 3
}

[CreateAssetMenu(fileName = "Item", menuName = "Item/Generic")]
public class Item : ScriptableObject, IComparable<Item>
{
    [SerializeField]
    private int _item_id;
    [SerializeField]
    private string _item_name;
    [SerializeField]
    private string _description;
    [SerializeField]
    private Sprite _img;
    [SerializeField]
    private bool _stackable;
    [SerializeField]
    private int _maxStacks;
    [SerializeField]
    private bool _consumable;
    [SerializeField]
    private bool _chargable;
    [SerializeField]
    private itemType _itemType;


    //getters and setters
    /*public itemType type
    {
        get { return _type; }
        set { _type = value; }
    }*/
    public int CompareTo(Item item)
    {   
        // A null value means that this object is greater.
        if (item == null){
            return 1;  
        }
        else {
            return this._item_id.CompareTo(item._item_id);
        }
    }

    public int item_id
    {
        get { return _item_id; }
        set { _item_id = value; }
    }

    public string item_name
    {
        get { return _item_name; }
        set { _item_name = value; }
    }
    public string description
    {
        get { return _description; }
        set { _description = value; }
    }

    public Sprite img
    {
        get { return _img; }
        set { _img = value; }
    }

    public bool stackable
    {
        get { return _stackable; }
        set { _stackable = value; }
    }

    public int maxStacks
    {
        get { return _maxStacks; }
        set { _maxStacks = value; }
    }

    public bool consumable
    {
        get { return _consumable; }
        set { _consumable = value; }
    }

    public bool chargable
    {
        get { return _chargable; }
        set { _chargable = value; }
    }

    public itemType ItemType
    {
        get { return _itemType; }
        set { _itemType = value; }
    }

    public virtual void use() { }
    public virtual void charge() { }

}
