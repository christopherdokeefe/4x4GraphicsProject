using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereAction : MonoBehaviour
{
    enum State {Attached, Detached};  // States of the sphere
    private State currentState = State.Attached;

    public SceneNode Base;
    public SceneNode Arm;
    public SceneNode Sling;

    public SceneNode[] hierarchy;

    private Vector3 launchDir;  // Direction of projectile being launched
    private float launchSpeed = 150f;   // Arbitrary launch speed
    private float gravity = -9.81f;     // Earth's gravity in meter's per second squared
    private float gravityFactor = 0.1f;  // Make gravity realistic

    void Start()
    {
        hierarchy = new SceneNode[] {Base, Arm, Sling};  // Used to calculate projectile position while attached to sling
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
    void attachedAction()
    {
        // WARNING: MY MP4 SOLUTION TO CALCULATE THE ENDPOINT OF THE LAST NODE
        transform.position = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.identity;

        Vector3 nodeScale = new Vector3(1, 1, 1);
        Quaternion nodeRotation = Quaternion.identity;
        Vector3 nodeTranslation = new Vector3(0, 0, 0);

        for(int i = 0; i < 3; i++)
        {
            Vector3 nodePosition = hierarchy[i].transform.localPosition;
            Vector3 nodeOrigin = hierarchy[i].NodeOrigin;

            nodeRotation *= hierarchy[i].transform.localRotation;
            nodeTranslation = hierarchy[i].transform.position;
            nodeScale = Vector3.Scale(nodeScale, hierarchy[i].transform.localScale);

            transform.position += transform.rotation * Vector3.Scale(nodeOrigin, nodeScale);
            transform.rotation = nodeRotation;
        }
        
        Vector3 baseScale = Base.transform.localScale;
        Vector3 armScale = Arm.transform.localScale;
        
        transform.position += nodeTranslation;
        transform.position += -Sling.PrimitiveList[0].transform.up * Sling.PrimitiveList[0].transform.localScale.y * 2;

        transform.position = Vector3.Scale(transform.position, baseScale);
        transform.position = Vector3.Scale(transform.position, armScale);

        // COULD NOT FIGURE OUT HOW TO GET TRANSFORM LOCALROTATION CORRECTLY
        // Once arm is approximately vertical, launch the ball tangent to the sling (sling's forward direction)
        if (Arm.transform.rotation.x < -0.8)
        {
            currentState = State.Detached;
            launchDir = Sling.PrimitiveList[0].transform.forward;
        }
        
    }

    void detachedAction()
    {
        // Move the ball based on launch speed and direction
        // Direction is constantly changing based on gravity to make it realistic
        transform.position += launchSpeed * launchDir * Time.deltaTime;
        launchDir = ((launchSpeed * launchDir) + (gravity * gravityFactor * Vector3.up)).normalized;
    }
}
