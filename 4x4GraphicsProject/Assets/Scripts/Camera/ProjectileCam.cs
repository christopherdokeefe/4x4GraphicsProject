using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCam : MonoBehaviour
{
    public GameObject Projectile;
    public SceneNode Base;
    private Vector3 dir;

    float cameraDistance = 20f;

    void Start()
    {
        UpdateDirection();
        transform.position = Projectile.transform.position - dir * cameraDistance;
        transform.position = new Vector3(transform.position.x, Projectile.transform.position.y + Projectile.transform.localScale.y * 2, transform.position.z);
    }

    void Update()
    {
        if (Projectile != null && Projectile.GetComponent<ProjectileAction>().GetAttached())
        {
            UpdateDirection();
        }
        if (Projectile != null)
        {
            updatePosition();
        }
    }

    void updatePosition()
    {
        transform.position = Projectile.transform.position - dir * cameraDistance;
        transform.position = new Vector3(transform.position.x, Projectile.transform.position.y + Projectile.transform.localScale.y, transform.position.z);
    }

    public void UpdateDirection()
    {
        dir = -Base.transform.forward;
        transform.forward = dir;
    }
}
