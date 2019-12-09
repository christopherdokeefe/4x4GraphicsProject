Shader "Unlit/451ShaderWithTexture"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		Cull Off

		Pass
		{
			CGPROGRAM
			#pragma vertex MyVert
			#pragma fragment MyFrag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 vertexWC : TEXCOORD3;
			};

			sampler2D _MainTex;

            // our own matrix
            float4x4 MyXformMat;  // our own transform matrix!!
            fixed4   MyColor;
			float4 LightPosition;
			
			v2f MyVert (appdata v)
			{
				v2f o;

                o.vertex = mul(MyXformMat, v.vertex);  // use our own transform matrix!
                    // MUST apply before camrea!
				o.uv = v.uv; // no specific placement support
                o.vertex = mul(UNITY_MATRIX_VP, o.vertex);   // camera transform only

				o.vertexWC = mul(UNITY_MATRIX_M, v.vertex); // this is in WC space!
				float3 p = v.vertex + v.normal;
				p = mul(UNITY_MATRIX_M, p);  // now in WC space
				o.normal = normalize(p + o.vertexWC); // NOTE: this is in the world space!!
				
                return o;
			}

			// our own function
			fixed4 ComputeDiffuse(v2f i) {
				float3 l = normalize(LightPosition - i.vertexWC);
				return clamp(dot(i.normal, l), 0, 1);
			}

			fixed4 MyFrag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);

				float diff = ComputeDiffuse(i);
				return col * diff;
			}
			ENDCG
		}
	}
}