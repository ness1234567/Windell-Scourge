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

    //places a dark highlight at X, Y with scale xScale, yScale
    public void activateHighlight(float x, float y, float xScale, float yScale)
    {
        //make highlight visible
        tf.localScale = new Vector3(xScale, yScale, 1);
        tf.localPosition = new Vector3(x, y, 0);
        highlight.color = new Color(0, 0, 0, 0.2f);
    }

    public void deactivateHighlight(int slotID)
    {
        Debug.Log("deactivate");
        //make highlight invisible
        tf.localScale = new Vector3(1, 1, 1);
        highlight.color = new Color(0, 0, 0, 0);
    }
}
