using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField]
    private List<Item> _ItemList = new List<Item>();

    public List<Item> ItemList
    {
        get { return _ItemList; }
        set { _ItemList = value; }
    }
}
