

/** @brief フォント。シンプル。
*/


Shader "Font/Simple"
{
    Properties
    {
		_MainTex	("_MainTex",2D)		= "white"{}
	}
    SubShader
    {
		Tags
		{
			"RenderType"	= "Transparent"
			"Queue"			= "Transparent"
		}
        Pass
        {
			Cull Off
			ZTest Always
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			/** appdata
			*/
			struct appdata
			{
				float4 vertex		: POSITION;
				float4 color		: COLOR0;
				float2 uv			: TEXCOORD0;
			};

			/** v2f
			*/
			struct v2f
			{
				float4 vertex		: SV_POSITION;
				float4 color		: COLOR0;
				float2 uv			: TEXCOORD0;
			};

			/** _MainTex
			*/
			sampler2D _MainTex;
			
			/** _Color
			*/
			fixed4 _Color;

			/** vert
			*/
			v2f vert(appdata a_appdata)
			{
				v2f t_ret;
				{
					t_ret.vertex = UnityObjectToClipPos(a_appdata.vertex);
					t_ret.color = a_appdata.color;
					t_ret.uv = a_appdata.uv;
				}
				return t_ret;
			}
			
			/** frag
			*/
			fixed4 frag(v2f a_v2f) : SV_Target
			{
				float4 t_color = a_v2f.color;
				t_color.a *= tex2D(_MainTex,a_v2f.uv).a;
				return t_color;
			}

            ENDCG
        }
    }
}

