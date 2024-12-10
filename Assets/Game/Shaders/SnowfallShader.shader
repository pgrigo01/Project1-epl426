Shader "Custom/SnowfallShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Speed("Fall Speed", Float) = 1.0
        _NoiseScale("Noise Scale", Float) = 5.0
    }
        SubShader
        {
            Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
            LOD 100

            Blend SrcAlpha OneMinusSrcAlpha

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                    float timeOffset : TEXCOORD1;
                };

                sampler2D _MainTex;
                float _Speed;
                float _NoiseScale;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    o.timeOffset = frac(_Time.y * _Speed + v.vertex.y); // Create time-based offset
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // Add noise for movement
                    float noise = frac(sin(i.timeOffset * 4321.0) * 12345.0);
                    float2 offset = float2(noise - 0.5, -i.timeOffset);

                    // Sample the texture with offset
                    fixed4 col = tex2D(_MainTex, i.uv + offset * _NoiseScale);
                    col.a *= 1.0 - i.timeOffset; // Fade as it falls
                    return col;
                }
                ENDCG
            }
        }
}
