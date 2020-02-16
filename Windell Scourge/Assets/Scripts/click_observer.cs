using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click_observer : MonoBehaviour
{

    private Camera mainCamera;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonUp(0))
        {
            Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hit = Physics2D.RaycastAll(mouseRay.origin, mouseRay.direction, 100f);

            if (hit != null)
            {
                //find first interactable object in raycast and call it's click event
                foreach (RaycastHit2D i in hit)
                {
                    //check if object of the hit collider is interactable
                    GameObject obj = i.transform.gameObject;
                    if (isInteractable(obj))
                    {
                        obj.GetComponent<ClickBehaviour>().onClick();
                        break;
                    }
                }
            }
        }*/
    }

    //TODO: check if in 1 tile range
    bool inRangeOfPlayer(GameObject player)
    {
        //find out which tile player is in

        //find out which tile this object is in

        //calculate min distance in tile
        return true;
    }

    bool isInteractable(GameObject o)
    {
        if((o.GetComponent<ClickBehaviour>() != null) && (inRangeOfPlayer(player)))
        {
            return true;
        }
        return false;
    }
}
