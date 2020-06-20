Shader "PBRLab/Mobile/EnvBRDFApproxNonmetal_Simple"
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
                half ndv = 1-max(dot(worldNormal, viewDir), 0);
                ndv *= ndv;
                ndv *= ndv;
                half oneMinusRoughness = 1-_Roughness;
                half envSpec = ndv*oneMinusRoughness*oneMinusRoughness + oneMinusRoughness*0.03 + 0.015;
                half3 result = LinearToGammaSpace(envSpec);
                return half4(result,1);
            }
            ENDCG
        }
    }
}
