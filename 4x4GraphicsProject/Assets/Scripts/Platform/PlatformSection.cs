using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSection : MonoBehaviour
{
    private bool shrinking;
    private float shrinkingRate = 0.9995f;

    void Start()
    { 
        shrinking = false;
    }

    void Update()
    {
        if (shrinking)
        {
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
        }
    }

    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(30);
        Destroy(gameObject);
    }
}
