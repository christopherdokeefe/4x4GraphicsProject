using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    float movespeed = 40f;
    float deltaRotation = 90f;

    float bobSpeed = 3f;
    float bobSpeedCurrent;

    float bobRange = 0.7f;
    float bobRangeCurrent = 0f;

    void Start()
    {
        bobSpeed += movespeed * 1 / 10;
        bobSpeedCurrent = movespeed;
    }

    void Update()
    {
        // Bob up and down to indicate movement
        if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {
            transform.position += new Vector3(0, bobSpeedCurrent, 0) * Time.deltaTime;
            bobRangeCurrent += bobSpeedCurrent * Time.deltaTime;
            if (bobRangeCurrent > bobRange)
            {
                bobSpeedCurrent = -bobSpeed;
            }
            else if (bobRangeCurrent < -bobRange)
            {
                bobSpeedCurrent = bobSpeed;
            }
        }

        if (Input.GetKey("w"))  // Move forward relative to the player
        {
            transform.position += movespeed * transform.forward * Time.deltaTime;
        }
        if (Input.GetKey("s"))  // Move backward relative to the player
        {
            transform.position += movespeed * -transform.forward * Time.deltaTime;
        }
        if (Input.GetKey("a"))  // Move left relative to the player
        {
            transform.position += movespeed * -transform.right * Time.deltaTime;
        }
        if (Input.GetKey("d"))  // Move right relative to the player
        {
            transform.position += movespeed * transform.right * Time.deltaTime;
        }

        if (Input.GetKey("q"))  // Rotate left relative to the player
        {
            transform.Rotate(0, -deltaRotation * Time.deltaTime, 0);
        }
        if (Input.GetKey("e"))  // Rotate right relative to the player
        {
            transform.Rotate(0, deltaRotation * Time.deltaTime, 0);
        }
    }
}
