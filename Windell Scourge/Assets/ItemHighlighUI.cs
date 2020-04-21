using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHighlighUI : MonoBehaviour
{
    RectTransform tf;

    float inv_initX = 9;
    float inv_initY = 50;

    float tool_initX = -91;
    float tool_initY = -69;

    [SerializeField]
    Image highlight;

    // Start is called before the first frame update
    void Start()
    {
        tf = this.GetComponent<RectTransform>();
        tf.localPosition = new Vector3(inv_initX, inv_initY);

        highlight = GetComponent<Image>();
        highlight.color = new Color(0, 0, 0, 0);    
    }

    public void activateHighlight(int slotID)
    {
        //move highlight image to slot position
        if ((slotID >= 0) && (slotID <= 29))
        {
            int row = (int)(slotID / 5);
            int col = slotID % 5;
            tf.localPosition = new Vector3(inv_initX + (col*20), inv_initY - (row*18));
        }
        else if ((slotID >= 30) && (slotID <= 39))
        {
            tf.localPosition = new Vector3(tool_initX + (20 * (slotID - 30)), tool_initY);
        }

        //make highlight visible
        highlight.color = new Color(0, 0, 0, 0.2f);
    }

    public void deactivateHighlight(int slotID)
    {
        Debug.Log("deactivate");
        //make highlight invisible
        highlight.color = new Color(0, 0, 0, 0);
    }
}
