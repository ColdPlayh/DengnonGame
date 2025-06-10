Shader "Bomber/MyShader"
{
	Properties
	{
		_MainTex("Texture",2D)="white"{}
		_MixedColor("MixedColor",Color)=(1,1,1,1)
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			sampler2D _MainTex;
			fixed4 _MixedColor;
			struct a2v
			{
				float4 obj_vert :POSITION;
				float2 uv :TEXCOORD;

				};
			struct v2f
			{
				float4 world_vert :SV_POSITION;
				float2 uv :TEXCOORD;
				};

			v2f vert(a2v v)
			{
				v2f f;
				f.world_vert=UnityObjectToClipPos(v.obj_vert);
				f.uv=v.uv;
				return f;
				}
			fixed4 frag(v2f f) :SV_Target
			{
				fixed4 final_color=tex2D(_MainTex,f.uv);
				final_color=final_color*_MixedColor;
				return final_color;
				}
			ENDCG
		}
		
	}
	FallBack "Diffuse"
}