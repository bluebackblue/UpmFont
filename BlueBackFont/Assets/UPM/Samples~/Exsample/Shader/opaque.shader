

/** @brief ïsìßñæÅB
*/


Shader "BlueBack/Gl/Exsample/opaque"
{
	Properties
	{
		_MainTex				("_MainTex",2D)									= "white"{}
	}
	SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
		}
		Pass
		{
			Cull Off
			ZTest Always
			ZWrite Off

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			/** appdata
			*/
			struct appdata
			{
				float4 vertex		: POSITION;
				float2 uv			: TEXCOORD0;
				float4 color		: COLOR0;
			};

			/** v2f
			*/
			struct v2f
			{
				float4 vertex		: SV_POSITION;
				float2 uv			: TEXCOORD0;
				float4 color		: COLOR0;
			};

			/** _MainTex
			*/
			sampler2D _MainTex;
			
			/** vert
			*/
			v2f vert(appdata a_appdata)
			{
				v2f t_ret;
				{
					t_ret.vertex = UnityObjectToClipPos(a_appdata.vertex);
					t_ret.uv = a_appdata.uv;
					t_ret.color = a_appdata.color;
				}
				return t_ret;
			}
			
			/** frag
			*/
			fixed4 frag(v2f a_v2f) : SV_Target
			{
				return tex2D(_MainTex,a_v2f.uv) * a_v2f.color;
			}

			ENDCG
		}
	}
}

