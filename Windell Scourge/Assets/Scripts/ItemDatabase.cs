using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static List<GameObject> items;
    
    void Awake()
    {
        BuildDatabase();
    }


    void BuildDatabase()
    {
        items = new List<GameObject>();

    }
    
}
