using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum transitionStates
{
    Locomotion = 0,
    UseItem = 1,
    Cutscene = 2,
}

public class playerController : MonoBehaviour
{
    private static playerController _instance;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator a;


    //currentDir = 0;
    //Item = 0;
    //usingItem = false;
    //interruptMovement = false;
    private bool invincible;
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

    void FixedUpdate()
    {
        if (a.GetBool("UnInterruptable") == false)
        {
            rb.MovePosition(rb.position + (direction * speed * Time.fixedDeltaTime));
        }
    }

    // Update is called once per frame
    void Update()
    {
        int test = a.GetInteger("CurrentDir");
        Debug.Log("currentDir = " + test);

        //check if time is running
        if (Time.timeScale == 0f)
            return;

        //check if currently in unskippable animation, if so, ignore all player inputs
        if (a.GetBool("UnInterruptable") == true)
        {
            speed = 0;
            return;
        }

        //Get Player Input

        //Update Player Animation

        //MOVEMENT CONTROLS
        updateMovement();

        //ITEM CONTROLS
        //use item
        if ((Input.GetKeyDown(KeyCode.Mouse0) == true) && (!EventSystem.current.IsPointerOverGameObject()))
        {
            //charge or use item 
            if (InventoryController.Instance.getCurrentItem() != null)
            {
                if (InventoryController.Instance.getCurrentItem().chargable)
                {
                    a.SetBool("ChargingItem", true);
                    a.SetInteger("Item", (int)InventoryController.Instance.getCurrentItem().ItemType);
                    InventoryController.Instance.chargeItem();
                }
                else
                {
                    InventoryController.Instance.useItem();
                }
                a.Play("UseItem.Empty");
            }
        }
        if (Input.GetKey(KeyCode.Mouse0) == false)
        {
            //if chargable item, use it on mouse release
            if (InventoryController.Instance.getCurrentItem() != null)
            {
                if (InventoryController.Instance.getCurrentItem().chargable && a.GetBool("ChargingItem") == true)
                {
                    int dir = a.GetInteger("CurrentDir");
                    Debug.Log("AT USE ITEM CALL: currentDir = " + dir);
                    a.SetBool("ChargingItem", false);

                    //Use Item when at correct Animation frame 
                    InventoryController.Instance.useItem();
                }
            }
        }
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
        if ((Input.GetKey(KeyCode.LeftShift) == true) || (a.GetBool("ChargingItem") == true))
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

    //////// GETTER AND SETTERS ////////////

    public Animator Animator
    {
        get { return a; }
        set { a = value; }
    }
}
