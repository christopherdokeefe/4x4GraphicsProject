using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLight : MonoBehaviour {
    public float Near = 5.0f;
    public float Far = 10.0f;
    public Color LightColor = Color.white;
    // Use the transform's position as light position

    public bool ShowLightRanges = false;


	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Renderer>().material.color = LightColor;
    }

    public void LoadLightToShader()
    {
        Shader.SetGlobalVector("LightPosition", transform.localPosition);
        Shader.SetGlobalColor("LightColor", LightColor);
        Shader.SetGlobalFloat("LightNear", Near);
        Shader.SetGlobalFloat("LightFar", Far);
    }
}
