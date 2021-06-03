using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpheight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 Velocity;
    bool isgrounded;
    public bool isCrouched;
    public bool isRunning;
    // Update is called once per frame
    void Update()
    {
        isgrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isgrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetAxis("Crouch") != 0 && isgrounded)
        {
            controller.transform.localScale = new Vector3(0.75f, 0.5f, 0.75f);

            isCrouched = true;
            speed = 2;

        }
        else
        {
            controller.height = 2f;
            isCrouched = false;
            speed = 5;
            controller.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.x, transform.localScale.z);
        }
        if (Input.GetAxis("Sprint") != 0 && isgrounded)
        { 
            isRunning = true;
            speed = 7;
        }
        else
        {
            isRunning = false;
            speed = 5;
         }
        if (Input.GetButtonDown("Jump") && isgrounded)
        {
            Velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
        }

        Velocity.y += gravity * Time.deltaTime;

        controller.Move(Velocity * Time.deltaTime);
    }

}
        // v = squr ( h * -2 * g)
