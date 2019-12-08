using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBase : MonoBehaviour
{
    public SceneNode Base;
    float deltaRotation = 90f;  // Degrees per second

    void Start()
    {

    }

    void Update()
    {
        // While holding down the "right" key, rotate left
        if (Input.GetKey(KeyCode.D))
        {
            rotateRight();
        }
        // While holding down the "left" key, rotate left
        if (Input.GetKey(KeyCode.A))
        {
            rotateLeft();
        }
    }

    // Rotate base to the right by negative deltaRotation
    void rotateRight()
    {
        Base.transform.Rotate(0, deltaRotation * Time.deltaTime, 0);
    }

    // Rotate base to the left by deltaRotation
    void rotateLeft()
    {
        Base.transform.Rotate(0, -deltaRotation * Time.deltaTime, 0);
    }
}
