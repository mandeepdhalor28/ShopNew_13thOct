﻿Shader "Versatile/Effect/Transparent_Jelly" {
	Properties
	 {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Effect("Effect",Range(0,0.5))=0.1
		_Freq("Speed",Range(0,5))=0.1
		
	}
	SubShader 
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True"}
		Blend SrcAlpha OneMinusSrcAlpha
		//Cull off
		//Zwrite off
		//ZTest Always
				
	pass
	{		
		CGPROGRAM
		#pragma exclude_renderers d3d11 xbox360
		#pragma vertex vert
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest
		#pragma multi_compile LIGHTMAP_ON LIGHTMAP_OFF
		#include "unityCG.cginc"
		
		

		sampler2D _MainTex;
		float _Freq;
		float _Effect;
		//fixed4 _Color;
		
		

		struct v2f
		 {
			float4 pos:SV_POSITION;
			
			float2 uv:TEXCOORD0;
			//float2 UV1:TEXCOORD1;
			//float2 UV2:TEXCOORD2;
			
			//fixed4 color:Color;
		};

		v2f vert(appdata_full v)
		{
		  v2f o;
		  fixed s=sin(_Time.y*_Freq)*_Effect+1.0f;
		  v.vertex.x *=s;
		  v.vertex.y /=s;
		  o.pos=mul(UNITY_MATRIX_MVP,v.vertex);
		  float2 uv=v.texcoord.xy;
		  o.uv=uv;
		  return o;
		}
		
		fixed4 frag(v2f i):COLOR
		{
		 fixed4 t1=tex2D(_MainTex,i.uv);
		 
		 return t1;
		}
		ENDCG
	  }//pass finish 
	}//subshader finish
	FallBack "jellyshader"
}//shader finish