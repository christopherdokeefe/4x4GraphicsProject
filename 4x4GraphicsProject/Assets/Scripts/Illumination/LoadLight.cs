using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLight : MonoBehaviour {
    public PointLight LightPosition;

	void OnPreRender()
    {
        Shader.SetGlobalVector("LightPosition", LightPosition.transform.localPosition);
    }
}
