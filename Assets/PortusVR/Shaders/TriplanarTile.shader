Shader "TriplanarTile"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Bump("Normal", 2D) = "bump" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        struct Input
        {
            float3 worldNormal; INTERNAL_DATA
            float3 worldPos;
        };

        sampler2D _MainTex, _Bump;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float4 _MainTex_ST, _Bump_ST;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        inline float4 triplanar(sampler2D tex, float3 position, float3 normal, float2 textureScale)
        {
            normal = abs(normal);
            float3 weights = normal / (normal.x + normal.y + normal.z);
            float4 x = tex2D(tex, position.yz * textureScale);
            float4 y = tex2D(tex, position.xz * textureScale);
            float4 z = tex2D(tex, position.xy * textureScale);
            return weights.x * x + weights.y * y + weights.z * z;
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float3 position = IN.worldPos;
            float3 normal = WorldNormalVector(IN, float3(0, 0, 1));

            float4 c = triplanar(_MainTex, position, normal, _MainTex_ST.xy) * _Color;
            float4 n = triplanar(_Bump, position, normal, _Bump_ST.xy);

            o.Normal = UnpackNormal(n);
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }

    FallBack "Diffuse"
}
