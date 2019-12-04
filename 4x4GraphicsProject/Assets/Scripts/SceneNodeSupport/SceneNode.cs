using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneNode : MonoBehaviour {

    protected Matrix4x4 mCombinedParentXform;
    
    public Vector3 NodeOrigin = Vector3.zero;
    public List<NodePrimitive> PrimitiveList;

    public Transform Projectile;
    private SphereAction sphereAction;

    private Vector3 projectilePosition = new Vector3(0f, -11f, -11f);
    private Vector3 initialPosition;
    private Vector3 initialScale;
    private Quaternion initialRotation;

    // Use this for initialization
    protected void Start()
    {
        InitializeSceneNode();
        initialPosition = transform.localPosition;
        initialScale = transform.localScale;
        initialRotation = transform.localRotation;
        if (Projectile != null)
            sphereAction = Projectile.GetComponent<SphereAction>();
        // Debug.Log("PrimitiveList:" + PrimitiveList.Count);

    }

    public void ResetPosition()
    {
        transform.localPosition = initialPosition;
        transform.localScale = initialScale;
        transform.localRotation = initialRotation;
    }

    private void InitializeSceneNode()
    {
        mCombinedParentXform = Matrix4x4.identity;
    }

    // This must be called _BEFORE_ each draw!! 
    public void CompositeXform(ref Matrix4x4 parentXform)
    {
        Matrix4x4 orgT = Matrix4x4.Translate(NodeOrigin);
        Matrix4x4 trs = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        
        mCombinedParentXform = parentXform * orgT * trs;

        // propagate to all children
        foreach (Transform child in transform)
        {
            SceneNode cn = child.GetComponent<SceneNode>();
            if (cn != null)
            {
                cn.CompositeXform(ref mCombinedParentXform);
            }
        }
        
        // disenminate to primitives
        foreach (NodePrimitive p in PrimitiveList)
        {
            if (p != null)
                p.LoadShaderMatrix(ref mCombinedParentXform);
        }

        // Checks to see if the scene node has a projectile and whether or not it's attached
        if (Projectile != null && sphereAction.GetAttached())
        {
            Vector3 newProjectilePosition = mCombinedParentXform.MultiplyPoint(projectilePosition);
            Projectile.localPosition = newProjectilePosition;
        }
    }

    public void SetProjectile(Transform projectile)
    {
        Projectile = projectile;
        sphereAction = projectile.GetComponent<SphereAction>();
    }
}