using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private static playerController _instance;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator a;
    private bool interruptMovement = false;

    [SerializeField]
    private float runSpeed = 7f;
    [SerializeField]
    private float walkSpeed = 2f;
    private float speed = 7f;

    private Vector2 direction;

    public static playerController Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0f)
        {
            return;
        }

        //MOVEMENT CONTROLS
        updateMovement();

        //ITEM CONTROLS
        //use item
        if (Input.GetKeyDown(KeyCode.Mouse0) == true)
        {
            InventoryController.Instance.useItem();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (direction * speed * Time.fixedDeltaTime));
    }

    void updateMovement()
    {
        //Get movement direction
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        direction.Normalize();
        direction.x = Mathf.Round(direction.x * 10f) / 10f;
        direction.y = Mathf.Round(direction.y * 10f) / 10f;

        //Check if moving
        if ((direction.x == 0) && (direction.y == 0))
        {
            a.SetBool("Moving", false);
            return;
        }
        else
        {
            a.SetBool("Moving", true);
            a.SetFloat("Horizontal", direction.x);
            a.SetFloat("Vertical", direction.y);
        }

        //Check if walking or running
        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            a.SetBool("Walk", true);
            speed = walkSpeed;
        }
        else
        {
            a.SetBool("Walk", false);
            speed = runSpeed;
        }
    }
}
