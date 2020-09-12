using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click_observer : MonoBehaviour
{

    private Camera mainCamera;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        player = playerController.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
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
                        obj.GetComponent<IClickBehaviour>().onClick(obj);

                    }
                }
            }
        }
    }

    //TODO: check if in 1 tile range
    bool inRangeOfPlayer(GameObject obj)
    {
        //find out point on Obj closest to player
        Vector3 pointOnObj = obj.GetComponent<Collider2D>().bounds.ClosestPoint(player.position);

        //calculate distance to player
        float xdist = Mathf.Abs(pointOnObj.x - player.position.x);
        float ydist = Mathf.Abs(pointOnObj.y - player.position.y);

        float reachDist = 1.5f;

        if ((xdist < reachDist) && (ydist < reachDist))
            return true;
        else
            return false;
    }

    bool isInteractable(GameObject o)
    {
        if((o.GetComponent<IClickBehaviour>() != null) && (inRangeOfPlayer(o)))
        {
            return true;
        }
        return false;
    }
}
