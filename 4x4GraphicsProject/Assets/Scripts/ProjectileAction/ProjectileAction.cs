using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileAction : MonoBehaviour
{
    enum State {Attached, Detached};  // States of the sphere
    private State currentState = State.Attached;

    public SceneNode sling;
    public SceneNode Base;

    public GameObject mainController;

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
        Base = GameObject.Find("BaseNode").GetComponent<SceneNode>();
        mainController = GameObject.Find("Controller");
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
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Base.transform.localEulerAngles.y, transform.localEulerAngles.z);
        transform.localEulerAngles = new Vector3(sling.transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    private void detachedAction()
    {
        // destroy projectile when it reaches 
        if (transform.position.y <= -300f)
        {
            Destroy(gameObject);
        }
    }

    public bool GetAttached()
    {
        return currentState == State.Attached;
    }

    public void DetatchObject()
    {
        currentState = State.Detached;

        if (mainController.GetComponent<RotateArm>().GetState() == "Launching")
            launchDir = sling.PrimitiveList[0].transform.forward;
        else
            launchDir = -sling.PrimitiveList[0].transform.forward; // Launch projectile backwards if its still attached while arm is resetting

        launchSpeed = PowerSlider.value;
        rb.velocity = (launchSpeed * launchDir);


        // Adds random spin to the projectiles for cool look and semi-realism
        Vector3 randomSpin = (new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f))).normalized;
        Vector3 tempLaunchDir = Vector3.Scale(randomSpin, launchDir);
        rb.AddTorque(tempLaunchDir * 2 * launchSpeed);

        // Start countdown for the projectile to be alive for a certain amount of time
        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(60);
        Destroy(gameObject);
    }
}
