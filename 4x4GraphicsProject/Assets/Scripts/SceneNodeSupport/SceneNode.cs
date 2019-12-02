using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneNode : MonoBehaviour {

    protected Matrix4x4 mCombinedParentXform;
    
    public Vector3 NodeOrigin = Vector3.zero;
    public List<NodePrimitive> PrimitiveList;

    public Transform AxisFrame = null;
    private Vector3 axisFramePosition = Vector3.zero;
    private Vector3 cameraPosition = new Vector3(0f, 4f, 0f);
    private Vector3 lookAtPosition = new Vector3(0f, 20f, 0f);
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

        // Compute AxisFrame 
        if (AxisFrame != null)
        {
            AxisFrame.localPosition = mCombinedParentXform.MultiplyPoint(axisFramePosition);
            AxisFrame.localRotation = transform.rotation;
        }
    }
}