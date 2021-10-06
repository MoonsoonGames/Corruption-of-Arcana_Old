using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 10.0F;
    public float jumpSpeed = 5.0F;
    public float gravity = 25.0F;
    private Vector3 moveDirection = Vector3.zero;
    private float turnCamera;
    private float lookCamera;
    public float sensitivity = 5;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        // Is controller connected to ground object
        if (controller.isGrounded)
        {
            //moveDiretion responds to movement input
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);

            //Multiply direction by speed value
            moveDirection *= moveSpeed;

            //Jumping
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

            //Sprinting
            if (Input.GetKey(KeyCode.LeftShift))
                moveSpeed = 10f;
            else
                moveSpeed = 5f;
        }

        //Mouse-Camera movement for 1st person movement (found online)
        turnCamera = Input.GetAxis("Mouse X") * sensitivity;
        if (turnCamera != 0)
        {
            //Code for action on mouse moving right
            transform.eulerAngles += new Vector3(0, turnCamera, 0);
        }

        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;

        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);
    }
}