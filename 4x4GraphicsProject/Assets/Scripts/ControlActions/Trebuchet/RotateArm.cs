using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArm : MonoBehaviour
{
    enum State {Stationary, Launching, Resetting};  // States of the trebuchet
    private State currentState = State.Stationary;

    public SceneNode Arm;
    public SceneNode Sling;

    public GameObject ProjectilePrefab;
    public ProjectileAction projectileAction; // Used to see if the projectile is attached 

    float end_time = 0;  // Keeps track of when the trebuchet can stop launching or resetting
    float fire_time = 1f;  // Time spent in "Launching" or "Resetting" state
    float deltaRotation = 180f;  // Degrees per second
    float slingRotationFactor = 0.9f; // Percent of deltaRotation used for the sling's rotation

    void Start()
    {
        projectileAction = Sling.GetProjectile().GetComponent<ProjectileAction>();
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
    
    // Trebuchet is stationary until "up" is pressed
    // This sets its state to "Launching" and it starts a launching timer
    void stationaryAction()
    {
        // Once the "W" key is pressed, the trebuchet starts launching
        if (Input.GetKey(KeyCode.W))
        {
            currentState = State.Launching;
            end_time = Time.realtimeSinceStartup + fire_time;
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
            // once the W key has been released, the projectile will launch
            if (Input.GetKeyUp(KeyCode.W))
            {
                projectileAction.DetatchObject();
            }
        }
        else
        {
            // launch the projectile if it hasn't been fired already
            if (projectileAction.GetAttached())
                projectileAction.DetatchObject();

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
            createProjectile();
        }
    }

    // Creates the projectile prefab and assigns it to SceneNode's projectile variable
    void createProjectile()
    {
        GameObject projectile = Instantiate(ProjectilePrefab) as GameObject;
        projectile.transform.parent = GameObject.Find("Projectiles").transform;
        Sling.SetProjectile(projectile.transform);
        projectileAction = projectile.GetComponent<ProjectileAction>();
        GameObject.Find("Trebuchet Camera").GetComponent<LoadLight>().LightPosition = projectile.GetComponent<PointLight>();

    }

    // Sets the projectile prefab to whatever dropdown option the user selected
    // and recreates the projectile if the trebuchet is stationary
    public void SetProjectilePrefab(GameObject prefab)
    {
        ProjectilePrefab = prefab;
        if (currentState == State.Stationary)  // Only replace projectile if it is stationary
            createProjectile();
    }

    // Returns currentState in the form of a string
    public string GetState()
    {
        return currentState.ToString();
    }
}
