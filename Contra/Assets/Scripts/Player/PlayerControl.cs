using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    //Movement Variables
    [SerializeField, Range (1,10)] private int speed = 5;
    [SerializeField, Range(1, 10)] private int jumpForce = 4;
    [SerializeField, Range(.01f, 1)] private float groundCheckRadius = .02f;
    [SerializeField] private LayerMask GroundLayer; 
    [SerializeField] private LayerMask WaterLayer;
    [SerializeField] private LayerMask DeathLayer;

    //Ground checks
    private Transform GroundCheck;
    private bool isGrounded = false; 
    private bool inWater = false;
    private bool inDeath = false;
    private bool isDown = false;
    private bool isFiring = false;

    // Start is called before the first frame update
    void Start()
    {
        TroubleShoot(); // Creates a Debug log to report garbage parameters

        //Creates a variable that uses a component reference
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (!GroundCheck) // Checks if Global variable 'GroundCheck' is null, and runs below script one if none exist
        {
            GameObject obj = new GameObject("Ground Check"); // Creates new GameObject named "Ground Check", assigns the variable obj, initializes it from an exisiting library.
            obj.transform.SetParent(transform); // Sets obj transforms to match the parents transforms. The Parent is what ever is running this script.
            obj.transform.localPosition = Vector3.zero; // Positions obj at local position 0,0,0 based on the origin (or pivot) point of the parent.
            GroundCheck = obj.transform; // Returns the transforms of obj to the global Variable "GroundCheck".
        }
    }
    void TroubleShoot() // If global variable is Less than or equal to 0, returns message in debug log.
    {
        if (speed <= 0) Debug.Log("Speed too low"); 
        if (jumpForce <= 0) Debug.Log("jumpForce too low");
        if (groundCheckRadius <= 0) Debug.Log("groundCHeckRadius too low");
    }

    void ResetBools()
    {
        isDown = false;
        isFiring = false;
    }

    // Update is called once per frame
    void Update()
    {
        surfaceType();
        

        float hInput = Input.GetAxis("Horizontal"); // Creates float Variable called hInput, assigns value based on input from the "horizontal" axis (unity input manager).
        float vInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);

        if (vInput <= -0.1f)
        {
            isDown = true;
        }
        else if (vInput >= 0)
        {
            isDown = false;
        }
        anim.SetFloat("vInput", Mathf.Abs(vInput));
        anim.SetBool("IsDown", isDown);

        if (Input.GetButtonDown("Fire2"))
        {
            isFiring = true;
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            isFiring = false;
        }
            anim.SetBool("IsFiring", isFiring);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Sprite flipping
        if (hInput != 0) sr.flipX = (hInput < 0); // if hInput does not = 0 then sprite renderer flip x will equal true or false based on inputed value +1 or -1
        
        //ResetBools();

        // Sets the Animator parameters to match and effectively be linked to this script
        anim.SetFloat("hInput", Mathf.Abs(hInput)); // Mathf.Abs() converts negative numbers into positive numbers. For the purpose of transitions all that's needed is yes or no
        anim.SetBool("IsGrounded", isGrounded); // Links the script bool variable to the animator logic bool.
        anim.SetBool("InWater", inWater);
        anim.SetBool("InDeath", inDeath);


    }
    void surfaceType() // Creates a Function called "surfaceType"
    {   if (!isGrounded) // Checks if isGrounded Global Variable is false, if not grounded (making this if == true) then move to nested if below.
        {
            if (rb.velocity.y <= 0) // Checks if character is falling or stationary on Y. 
            {               // 'Physics2D.OverlapCircle(Coordinates of obj, Size of obj defined in game, what layer mask is being monitored)'
                isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, GroundLayer);
            }
        }       
        else
            isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, GroundLayer); // Does the in game obj conflict with objects within the layer selected?
        // Check if the character is in water
        inWater = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, WaterLayer);

        // Check if the character is in a death zone
        inDeath = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, DeathLayer);
    }

}
