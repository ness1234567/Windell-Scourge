using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildDatabase : MonoBehaviour
{
    [MenuItem("ItemDatabase/BuildData")]
    static void BuildDatabaseSO()
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
