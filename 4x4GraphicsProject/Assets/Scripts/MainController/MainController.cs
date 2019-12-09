using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public Text Timer;
    float time = 60f;
    float rbGravityFactor = 8f;  // Multiply gravity constant for all rigidbodies by this amount

    void Start()
    {
        Physics.gravity *= rbGravityFactor;  // Adjust gravity of all rigidbodies since trebuchet is really big
    }

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            Timer.text = "Time: " + time.ToString("F1");
        }
        else
        {
            
        }

    }
}
