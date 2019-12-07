using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public GameObject Player;
    Vector3 delta = Vector3.zero;
    Vector3 mouseDownPos = Vector3.zero;

    float tumbleSpeed = 1f / 5f;

    float cameraDistance = 60f;

    void Start()
    {

    }

    void Update()
    {
        updateViewAngle();
        updateLookAt();

        // Tumble the camera (rotating around the Player)
        mouseDownPos = Input.mousePosition;
        delta = Vector3.zero;

        delta = mouseDownPos - Input.mousePosition;
        mouseDownPos = Input.mousePosition;
        ProcessTumble(delta * tumbleSpeed);
    }

    void updateLookAt()
    {
        Vector3 dir = (Player.transform.localPosition - transform.localPosition).normalized;
        transform.forward = dir;
    }

    void updateViewAngle()
    {
        Vector3 dir = Player.transform.forward;
        transform.position = Player.transform.position - dir * cameraDistance;
        transform.position = new Vector3(transform.position.x, Player.transform.position.y + Player.transform.localScale.y * 2, transform.position.z);
    }

    public void ProcessTumble(Vector3 delta)
    {
        Vector3 v = (Player.transform.localPosition - transform.localPosition);
        float dist = v.magnitude;

        // delta.y represents rotating around x-axis
        // -delta.x represents rotating around y-axis in proper direction
        transform.Rotate(delta.y, -delta.x, 0f);

        if (transform.eulerAngles.x > 80 && transform.eulerAngles.x <= 180) 
        { 
            transform.eulerAngles = new Vector3(80, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        else if (transform.eulerAngles.x > 180 && transform.eulerAngles.x < 280)
        {
            transform.eulerAngles = new Vector3(-80, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        Vector3 dir = transform.forward;
        transform.position = Player.transform.position - (dir * dist);
    }
}
