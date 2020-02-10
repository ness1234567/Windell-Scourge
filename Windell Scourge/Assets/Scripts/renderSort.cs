using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renderSort : MonoBehaviour
{
    private int sortingOrderBase = 16000;
    [SerializeField]
    private int offset = 0;
    [SerializeField]
    private int currentSortOrder = 0;
    private Renderer myRenderer;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y*16 - offset);
        currentSortOrder = myRenderer.sortingOrder;
    }
}
