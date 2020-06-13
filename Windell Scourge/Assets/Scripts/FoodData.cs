using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Item/Food")]
public class FoodData : ItemData
{
    [SerializeField]
    private int healthRestored;


}
