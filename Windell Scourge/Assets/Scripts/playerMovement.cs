using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator a;
    public float speed = 5;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        if((direction.x == 0) && (direction.y == 0))
        {
            a.SetBool("Moving", false);
        } else
        {
            a.SetBool("Moving", true);
            a.SetFloat("Horizontal", direction.x);
            a.SetFloat("Vertical", direction.y);
        }
        if(Input.GetKey(KeyCode.LeftShift) == true)
        {
            a.SetBool("Walk", true);
            speed = 1.5f;
        } else
        {
            a.SetBool("Walk", false);
            speed = 5;
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

}
