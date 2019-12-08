using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float MoveSpeed = 50f;
    public float JumpPower = 20f;

    private float bobSpeed = 10f;
    private float bobSpeedCurrent;

    private float bobRange = 0.7f;
    private float bobRangeCurrent = 0f;

    private Vector3 delta = Vector3.zero;
    private Vector3 mouseDownPos = Vector3.zero;
    private float deltaModifier = 0.1f;
    
    private bool canJump;

    private Rigidbody rb;

    void Start()
    { 
        bobSpeed += MoveSpeed * 1 / 10;
        bobSpeedCurrent = MoveSpeed;
        rb = transform.GetComponent<Rigidbody>();
        canJump = true;
    }

    void Update()
    {
        /*
        // Bob up and down to indicate movement
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.position += new Vector3(0, bobSpeedCurrent, 0) * Time.deltaTime;
            bobRangeCurrent += bobSpeedCurrent * Time.deltaTime;
            if (bobRangeCurrent > bobRange)
            {
                bobSpeedCurrent = -bobSpeed;
            }
            else if (bobRangeCurrent < -bobRange)
            {
                bobSpeedCurrent = bobSpeed;
            }
        } */

        // Handles movement
        if (Input.GetKey(KeyCode.UpArrow))  // Move forward relative to the player
        {
            rb.position += MoveSpeed * transform.forward * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))  // Move backward relative to the player
        {
            rb.position += MoveSpeed * -transform.forward * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))  // Move left relative to the player
        {
            rb.position += MoveSpeed * -transform.right * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))  // Move right relative to the player
        {
            rb.position += MoveSpeed * transform.right * Time.deltaTime;
        }

        // Rotation of player that rotates the camera (rotating around the Player)
        if (Input.GetMouseButtonDown(1))
        {
            mouseDownPos = Input.mousePosition;
            delta = Vector3.zero;
        }
        if (Input.GetMouseButton(1))
        {
            delta = mouseDownPos - Input.mousePosition;
            mouseDownPos = Input.mousePosition;
            ProcessRotation(delta * deltaModifier);
        }

        // Handles jump
        if (Input.GetMouseButtonDown(0))
        {
            if (canJump)
            {
                rb.AddForce(new Vector3(0, JumpPower, 0));
                canJump = false;
            }
        }
    }

    void ProcessRotation(Vector3 delta)
    {
        transform.Rotate(0, delta.x, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Platform"))
        {
            canJump = true;
        }
    }
}
