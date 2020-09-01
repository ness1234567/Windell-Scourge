using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField]
    private int thrustForce;
    [SerializeField]
    private float knockbackDuration;
    private Collider2D playerColl;
    private Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        playerColl = playerController.Instance.GetComponent<Collider2D>();
        playerRB = playerController.Instance.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Knock enemy back if enemy is movable
        if (other.gameObject.CompareTag("enemy"))
        {
        }
        //Knock player back if the hit object is immovable
        else if (other.gameObject.CompareTag("immovable"))
        {
            Vector3 pointOnObj = other.bounds.ClosestPoint(playerColl.transform.position);
            Vector3 pointOnPlayer = playerColl.bounds.ClosestPoint(other.transform.position);
            Vector2 PushDir = pointOnPlayer - pointOnObj;

            int currDir = playerController.Instance.Animator.GetInteger("CurrentDir");
            //knockback in y direction
            if (currDir == 0)
            {
                PushDir.x = 0;
                PushDir.y = 1 - Mathf.Abs(PushDir.y);
                Debug.Log(" PushDir.y middle = " + PushDir.y);

                if (Mathf.Abs(PushDir.y) < 0.6)
                    return;
                PushDir.y = PushDir.y - 0.6f;
            }
            else if (currDir == 2)
            {
                PushDir.x = 0;
                PushDir.y = -1 + Mathf.Abs(PushDir.y);
                if (Mathf.Abs(PushDir.y) < 0.6)
                    return;
                PushDir.y = PushDir.y + 0.6f;
            }
            //knockback in x direction
            else if (currDir == 1)
            {
                PushDir.y = 0;
                PushDir.x = 1 - Mathf.Abs(PushDir.x);
                if (Mathf.Abs(PushDir.x) < 0.6)
                    return;
                PushDir.x = PushDir.x - 0.6f;
            }
            else if (currDir == 3)
            {
                PushDir.y = 0;
                PushDir.x = -1 + Mathf.Abs(PushDir.x);
                if (Mathf.Abs(PushDir.x) < 0.6)
                    return;
                PushDir.x = PushDir.x + 0.6f;
            }

            PushDir = PushDir * thrustForce;
            playerRB.AddForce(PushDir, ForceMode2D.Impulse);
            StartCoroutine(knockTimer(playerRB));
        }
    }

    private IEnumerator knockTimer(Rigidbody2D body)
    {
        if(body != null)
        {
            yield return new WaitForSeconds(knockbackDuration);
            body.velocity = Vector2.zero;
        }
    }
}
