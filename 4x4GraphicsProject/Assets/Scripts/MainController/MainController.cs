using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    float rbGravityFactor = 8f;  // Multiply gravity constant for all rigidbodies by this amount

    void Start()
    {
        Physics.gravity *= rbGravityFactor;  // Adjust gravity of all rigidbodies since trebuchet is really big
    }

    void Update()
    {
        
    }
}
