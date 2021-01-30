using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player_Movement : MonoBehaviour
{
    [Header("Adjustable Variables")]
    
    [Tooltip("Set the Walking movement speed")]
    public float walkSpeed = 9.0f;

    [Tooltip("Set the Running movement speed")]
    public float runSpeed = 15.0f;   

    [Space]
    public float gravity = 40f;
    public float jumpForce = 15.0f;
    
    
    
    public LayerMask groundMask;
    public Transform groundCheck;

    private float groundDistance = 0.4f;
    private bool isGrounded;
    private float verticalVelocity;
    private float moveSpeed;
    private float originalHeight;    

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
        moveSpeed = walkSpeed;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ground check

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && verticalVelocity < 0)
        {
            controller.slopeLimit = 45.0f;
            verticalVelocity = -9.8f;
        }

        //Moves the player Left/right/front/back based on input
        float x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float z = Input.GetAxisRaw("Vertical") * moveSpeed;

        // Gravity
        verticalVelocity -= gravity * Time.deltaTime;


        Vector3 moveVector = new Vector3(x, verticalVelocity, z);
        moveVector = transform.TransformDirection(moveVector);
        controller.Move(moveVector * Time.deltaTime);


        // PLAYER CONTROLS //
        // Mouse interaction of objects will be handled by another script

        // JUMP
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            verticalVelocity = jumpForce;
        }
               
        // Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            moveSpeed = runSpeed;
            
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) )
        {
            moveSpeed = walkSpeed;
        }

        // TODO:
        // - Tune mouse look speed
        // - tune mouse look min/max angles
        // - Tune movement speeds
        // get controller working with platforms
        // get interactivity working
        // add mouse aim cursor



    }
}
       
    
