Shader "PBRLab/Mobile/EnvBRDFApproxNonmetal_UE4"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Roughness ("Roughness", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "PBRLib.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
            };

            half _Roughness;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldDir(v.normal);
                half3 worldPos = mul(unity_ObjectToWorld,v.vertex);
                o.viewDir = normalize(_WorldSpaceCameraPos.xyz - worldPos);
                return o;
            }

            fixed4 frag (v2f input) : SV_Target
            {
                half3 worldNormal = input.worldNormal.rgb;
                half3 viewDir = input.viewDir.rgb;
		        half3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                half ndv = max(dot(worldNormal, viewDir), 0);
                half envSpec = EnvBRDFApproxNonmetal_UE4(_Roughness, ndv);
                half3 result = LinearToGammaSpace(envSpec);
                return half4(result,1);
            }
            ENDCG
        }
    }
}
