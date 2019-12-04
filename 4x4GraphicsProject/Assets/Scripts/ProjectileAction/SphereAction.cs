using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereAction : MonoBehaviour
{
    enum State {Attached, Detached};  // States of the sphere
    private State currentState = State.Attached;

    public SceneNode sling;

    public SceneNode[] hierarchy;

    private Vector3 launchDir;  // Direction of projectile being launched
    private float launchSpeed = 150f;   // Arbitrary launch speed
    private float gravity = -9.81f;     // Earth's gravity in meter's per second squared
    private float gravityFactor = 0.1f;  // Make gravity realistic

    void Start()
    {
        currentState = State.Attached;
        sling = GameObject.Find("SlingNode").GetComponent<SceneNode>();
    }

    void Update()
    {
        switch(currentState)
        {
            case State.Attached:
                attachedAction();
                break;
            case State.Detached:
                detachedAction();
                break;
            default:
                break;
        }
    }

    // Keeps the projectile attached to the end of the sling
    private void attachedAction()
    {
        // COULD NOT FIGURE OUT HOW TO GET TRANSFORM LOCALROTATION CORRECTLY
        // Once arm is approximately vertical, launch the ball tangent to the sling (sling's forward direction)
        //if (Arm.transform.localRotation.x < -0.8)
        if (Input.GetKeyUp(KeyCode.W))
        {
            currentState = State.Detached;
            launchDir = sling.PrimitiveList[0].transform.forward;
        }
    }

    private void detachedAction()
    {
        // Move the ball based on launch speed and direction
        // Direction is constantly changing based on gravity to make it realistic
        transform.position += launchSpeed * launchDir * Time.deltaTime;
        launchDir = ((launchSpeed * launchDir) + (gravity * gravityFactor * Vector3.up)).normalized;

        // if the projectile either hits the platform/player (NOT IMPLEMENTED YET) or reaches a certain y value
        // attatch the projectile back
        if (transform.position.y <= -50f)
        {
            //currentState = State.Attached;
            Destroy(gameObject);
        }
    }

    public bool GetAttached()
    {
        return currentState == State.Attached;
    }
}
