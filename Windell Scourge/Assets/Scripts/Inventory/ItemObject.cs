using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class ItemObject : ScriptableObject
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

    //getters and setters
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
}
