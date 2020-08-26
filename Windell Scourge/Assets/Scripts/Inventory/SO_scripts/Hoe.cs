using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hoe", menuName = "Item/Hoe")]
public class Hoe : Item
{
    [SerializeField]
    private int _ToolTier;

    public override void use() {
        Debug.Log("Using Hoe!");

        playerController player = playerController.Instance;


    }

}
