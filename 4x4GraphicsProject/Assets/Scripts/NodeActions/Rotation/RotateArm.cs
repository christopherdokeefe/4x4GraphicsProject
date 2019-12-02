using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArm : MonoBehaviour
{
    public SceneNode Arm;
    float deltaRotation = 180f;  // Degrees per second

    void Start()
    {

    }

    void Update()
    {
        // While holding down the "a" key, rotate left
        if (Input.GetKey("w"))
        {
            rotateForward();
        }
        // While holding down the "d" key, rotate left
        if (Input.GetKey("s"))
        {
            rotateBackward();
        }
    }

    // Rotate base to the right by negative deltaRotation
    void rotateForward()
    {
        Arm.transform.Rotate(-deltaRotation * Time.deltaTime, 0, 0);
    }

    // Rotate base to the left by deltaRotation
    void rotateBackward()
    {
        Arm.transform.Rotate(deltaRotation * Time.deltaTime, 0, 0);
    }
}
