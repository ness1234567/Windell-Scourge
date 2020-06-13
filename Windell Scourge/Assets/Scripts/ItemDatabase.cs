using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> items;
    /*
    void Awake()
    {
        BuildDatabase();
    }


    void BuildDatabase()
    {
        items = new List<ItemData>();
        //TODO
    }*/
    
}
