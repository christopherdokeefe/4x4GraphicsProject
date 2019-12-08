using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSection : MonoBehaviour
{
    private bool shrinking;
    private float shrinkingRate = 0.9992f;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody rb;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        initialPosition = rb.position;
        initialRotation = transform.rotation;
        shrinking = false;
    }

    void Update()
    {
        
        if (shrinking)
        {
            
            rb.position = initialPosition;
            transform.rotation = initialRotation;
            Vector3 newScale = transform.localScale;
            newScale *= shrinkingRate;
            transform.localScale = newScale;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag.Equals("Projectile"))
        {
            shrinking = true;
            StartCoroutine(DestroyGameObject());
            rb.isKinematic = false;
        }
    }

    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }
}
