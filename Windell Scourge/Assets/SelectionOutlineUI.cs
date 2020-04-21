using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionOutlineUI : MonoBehaviour
{
    int baseSlotID = 30;
    int currentSlotID;
    float initX = -90;
    float initY = -2;
    [SerializeField]
    RectTransform tf;

    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<RectTransform>();
        tf.localPosition = new Vector3(initX, initY);
        currentSlotID = baseSlotID;
    }

    // Update is called once per frame
    void Update()
    {
        //poll for Keyboard inpute 1-10
        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentSlotID = baseSlotID + 0;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            currentSlotID = baseSlotID + 1;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            currentSlotID = baseSlotID + 2;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            currentSlotID = baseSlotID + 3;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            currentSlotID = baseSlotID+4;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            currentSlotID = baseSlotID + 5;
        if (Input.GetKeyDown(KeyCode.Alpha7))
            currentSlotID = baseSlotID + 6;
        if (Input.GetKeyDown(KeyCode.Alpha8))
            currentSlotID = baseSlotID + 7;
        if (Input.GetKeyDown(KeyCode.Alpha9))
            currentSlotID = baseSlotID + 8;
        if (Input.GetKeyDown(KeyCode.Alpha0))
            currentSlotID = baseSlotID + 9;

        //poll for mouse scroll wheel input

        //Debug.Log(currentSlotID);
        //Update UI current tool outline position
        tf.localPosition = new Vector3(initX + (20 * (currentSlotID - 30)), initY);

        //send to model the current highlighted tool


    }
}
