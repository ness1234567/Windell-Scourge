using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class SelectionOutlineUI : MonoBehaviour
{
    //Selection outline properties
    float initX = -90;
    float initY = -2;

    //The selection outline
    [SerializeField]
    RectTransform tf;

    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<RectTransform>();
        tf.localPosition = new Vector3(initX, initY);
    }

    //Update UI current tool outline position
    public void updatePos(int slotID)
    {
        tf.localPosition = new Vector3(initX + (20 * slotID), initY);
    }
}
