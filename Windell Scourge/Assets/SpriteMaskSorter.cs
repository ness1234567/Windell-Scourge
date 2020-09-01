using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskSorter : MonoBehaviour
{
    [SerializeField]
    private SpriteMask sm;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private Transform chestTransform;
    [SerializeField]
    float x1;

    [SerializeField]
    float x2;

    [SerializeField]
    private int bias;


    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponentInParent<SpriteRenderer>();
        sm = this.GetComponentInParent<SpriteMask>();

        int chest_RenderOrder = sr.sortingOrder;
        sm.backSortingOrder = chest_RenderOrder - bias;
        sm.frontSortingOrder = chest_RenderOrder;

    }

    void Update()
    {
        int chest_RenderOrder = sr.sortingOrder;

        sm.backSortingOrder = chest_RenderOrder - bias;
        sm.frontSortingOrder = chest_RenderOrder;

        float PlayerX = playerController.Instance.transform.position.x;
        float PlayerY = playerController.Instance.transform.position.y;

        x1 = sr.sprite.rect.xMin;
        x2 = sr.sprite.rect.xMax;
        float y1 = sr.sprite.rect.yMin;
        float y2 = sr.sprite.rect.yMax;

        if (((PlayerX > x1) && (PlayerX < x2)) && ((PlayerY > y1) && (PlayerY < y2)))
        {
            sm.enabled = true;
        }
        else
        {
            sm.enabled = false;
        }
    }

}
