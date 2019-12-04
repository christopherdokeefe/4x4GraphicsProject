﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArm : MonoBehaviour
{
    enum State {Stationary, Launching, Resetting};  // States of the trebuchet
    private State currentState = State.Stationary;

    public SceneNode Arm;
    public SceneNode Sling;

    public SphereAction sphereAction; // Used to see if the projectile is attached 

    private float end_time = 0;  // Keeps track of when the trebuchet can stop launching or resetting
    private float fire_time = 1f;  // Time spent in "Launching" or "Resetting" state
    float deltaRotation = 180f;  // Degrees per second
    float slingRotationFactor = 0.9f; // Percent of deltaRotation used for the sling's rotation

    void Start()
    {
        
    }

    void Update()
    {
        switch(currentState)
        {
            case State.Stationary:
                stationaryAction();
                break;
            case State.Launching:
                launchingAction();
                break;
            case State.Resetting:
                resettingAction();
                break;
            default:
                break;
        }
    }
    
    // Trebuchet is stationary until "w" is pressed
    // This sets its state to "Launching" and it starts a launching timer
    void stationaryAction()
    {
        // Once the "w" key is pressed, the trebuchet starts launching
        if (Input.GetKeyDown("w"))
        {
            if (sphereAction != null && sphereAction.GetAttached())
            {
                currentState = State.Launching;
                end_time = Time.realtimeSinceStartup + fire_time;
            }
        }
    }

    // Until the end_time is reached, the arm is rotated forward
    // Once the time is up, it resets the timer and sets its state to "Resetting"
    void launchingAction()
    {
        if (end_time > Time.realtimeSinceStartup)
        {
            Arm.transform.Rotate(-deltaRotation * Time.deltaTime, 0, 0);
            Sling.transform.Rotate(-deltaRotation * slingRotationFactor * Time.deltaTime, 0, 0);
        }
        else
        {
            currentState = State.Resetting;
            end_time = Time.realtimeSinceStartup + fire_time;
        }
    }

    // Does the same thing as launchingAction, but in reverse to reset its position
    // Once the time is up, it sets its state back to "Stationary"
    void resettingAction()
    {
        if (end_time > Time.realtimeSinceStartup)
        {
            Arm.transform.Rotate(deltaRotation * Time.deltaTime, 0, 0);
            Sling.transform.Rotate(deltaRotation * slingRotationFactor * Time.deltaTime, 0, 0);
        }
        else
        {
            currentState = State.Stationary;
        }
    }
}
