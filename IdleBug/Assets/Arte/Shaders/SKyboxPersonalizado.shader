Shader "Skybox/personalizado"
{
    Properties
    {
		_SkyTint("Sky Tint", Color) = (0.500000,0.500000,0.500000,1.000000)
    }
    SubShader
    {
        Tags { "QUEUE" = "Background" "RenderType" = "Background" "PreviewType" = "Skybox" }
        LOD 100

        Pass
        {
		 Tags { "QUEUE" = "Background" "RenderType" = "Background" "PreviewType" = "Skybox" }
			ZWrite Off
			Cull Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

			fixed4 _SkyTint;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = _SkyTint;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
				fixed4 col = _SkyTint;
                // apply fog
                return col;
            }
            ENDCG
        }
    }
}
