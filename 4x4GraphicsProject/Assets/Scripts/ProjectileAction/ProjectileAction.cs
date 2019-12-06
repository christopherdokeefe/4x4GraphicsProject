using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileAction : MonoBehaviour
{
    enum State {Attached, Detached};  // States of the sphere
    private State currentState = State.Attached;

    public SceneNode sling;

    public Slider PowerSlider;

    private Vector3 launchDir;  // Direction of projectile being launched
    private float launchSpeed;   // Projectile launch speed
    private float gravity = -9.81f;     // Earth's gravity in meter's per second squared
    private float gravityFactor = 0.1f;  // Make gravity realistic

    private Rigidbody rb;  // Projectile's rigidbody
                           // Gravity is changed in RotateArm
    void Start()
    {
        currentState = State.Attached;
        sling = GameObject.Find("SlingNode").GetComponent<SceneNode>();
        PowerSlider = GameObject.Find("PowerSlider").GetComponent<Slider>();
        rb = GetComponent<Rigidbody>();
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

    // Keeps the projectile attached to the end of the sling until "w" is released
    private void attachedAction()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            currentState = State.Detached;
            launchDir = sling.PrimitiveList[0].transform.forward;
            launchSpeed = PowerSlider.value;
            rb.AddForce(launchSpeed * 50 * launchDir);
            
            Vector3 randomSpin = (new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f))).normalized;
            Vector3 tempLaunchDir = Vector3.Scale(randomSpin, launchDir);
            rb.AddTorque(tempLaunchDir * 2 * launchSpeed);
        }
    }

    private void detachedAction()
    {
        // Move the ball based on launch speed and direction
        // Direction is constantly changing based on gravity to make it realistic
        
        /* transform.position += launchSpeed * launchDir * Time.deltaTime; */
        // rb.MovePosition(transform.position + (launchSpeed * launchDir * Time.deltaTime));
        // launchDir = ((launchSpeed * launchDir) + (gravity * gravityFactor * Vector3.up)).normalized;

        // if the projectile either hits the platform/player (NOT IMPLEMENTED YET) 
        // or reaches a certain y value, destroy the projectile
        if (transform.position.y <= -50f)
        {
            Destroy(gameObject);
        }
    }

    public bool GetAttached()
    {
        return currentState == State.Attached;
    }
}
