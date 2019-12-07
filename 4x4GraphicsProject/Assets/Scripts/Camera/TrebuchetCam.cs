using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrebuchetCam : MonoBehaviour
{
    public GameObject LookAtPosition;
    public SceneNode Base;

    float cameraDistance = 80f;

    void Update()
    {
        updateViewAngle();
        updateLookAt();
    }

    void updateLookAt()
    {
        Vector3 dir = (LookAtPosition.transform.localPosition - transform.localPosition).normalized;
        transform.forward = dir;
    }

    void updateViewAngle()
    {
        Vector3 dir = Base.transform.forward;
        transform.position = Base.transform.position + dir * cameraDistance;
        transform.position = new Vector3(transform.position.x, LookAtPosition.transform.position.y, transform.position.z);
    }
}
