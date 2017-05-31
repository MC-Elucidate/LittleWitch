Shader "Effects/ExpandingSphereShader" {
	Properties{
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
	}
		SubShader{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		LOD 200

		CGPROGRAM
#pragma surface surf Lambert alpha

		fixed4 _Color;

	struct Input {
		float2 uv_MainTex;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		o.Albedo = _Color.rgb;
		o.Emission = _Color.rgb; // * _Color.a;
		o.Alpha = _Color.a;
	}
	ENDCG
	}
}
