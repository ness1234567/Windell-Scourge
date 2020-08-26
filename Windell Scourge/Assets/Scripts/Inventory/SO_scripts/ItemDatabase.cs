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

    [MenuItem("ItemDatabase/BuildData")]
    static void BuildDatabase()
    {
        Debug.Log("building Database");
        ItemDatabase database = Resources.Load<ItemDatabase>("ItemDatabase");
        Item[] foundItems = Resources.LoadAll<Item>("UsableItems");

        database.ItemList.Clear();
        foreach (Item i in foundItems)
        {
            database.ItemList.Add(i);
        }

        //sort list by id
        database.ItemList.Sort();
    }
}
