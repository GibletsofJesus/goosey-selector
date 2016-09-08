Shader "Custom/background" {
	Properties{
		_MainTex("SelfIllum Color (RGB) Alpha (A)", 2D) = "white"{}
		_TintColour("Tint Colour",color) = (1,1,1,1)
	}
		SubShader{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		Lighting Off
		ZWrite Off
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		//#pragma surface surf Lambert  //Instead of this line add the next 8
#pragma surface surf NoLighting 
		fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
	{
		fixed4 c;
		c.rgb = s.Albedo;
		c.a = s.Alpha;
		return c;
	}
	struct Input {
		float2 uv_MainTex;
	};
	sampler2D _MainTex;
	fixed4 _TintColour;
	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = c.rgb*_TintColour.rgb;
		o.Alpha = _TintColour.a;
	}

	ENDCG
	}
		Fallback "Diffuse"
}