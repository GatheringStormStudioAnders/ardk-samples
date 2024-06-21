Shader "Custom/arShadows"
{
    Properties
    {
        _ShadowColor("Shadow Color", Color) = (0,0,0,0)
        _ShadowStrength("Shadow Strength", Range(0 , 1)) = 0.5882353
        [HideInInspector] __dirty("", Int) = 1
    }
        SubShader
    {
        Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
        Cull Back
        Blend SrcAlpha OneMinusSrcAlpha , SrcAlpha OneMinusSrcAlpha
        BlendOp Add
        ColorMask RGB
        CGPROGRAM
        #include "UnityPBSLighting.cginc"
        #pragma target 3.0
        #pragma surface surf StandardCustomLighting keepalpha noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
        struct Input
        {
            half filler;
        };
        struct SurfaceOutputCustomLightingCustom
        {
            half3 Albedo;
            half3 Normal;
            half3 Emission;
            half Metallic;
            half Smoothness;
            half Occlusion;
            half Alpha;
            Input SurfInput;
            UnityGIInput GIData;
        };
        uniform float _ShadowStrength;
        uniform float4 _ShadowColor;
        inline half4 LightingStandardCustomLighting(inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi)
        {
            UnityGIInput data = s.GIData;
            Input i = s.SurfInput;
            half4 c = 0;
            #ifdef UNITY_PASS_FORWARDBASE
            float ase_lightAtten = data.atten;
            if (_LightColor0.a == 0)
            ase_lightAtten = 0;
            #else
            float3 ase_lightAttenRGB = gi.light.color / ((_LightColor0.rgb) + 0.000001);
            float ase_lightAtten = max(max(ase_lightAttenRGB.r, ase_lightAttenRGB.g), ase_lightAttenRGB.b);
            #endif
            #if defined(HANDLE_SHADOWS_BLENDING_IN_GI)
            half bakedAtten = UnitySampleBakedOcclusion(data.lightmapUV.xy, data.worldPos);
            float zDist = dot(_WorldSpaceCameraPos - data.worldPos, UNITY_MATRIX_V[2].xyz);
            float fadeDist = UnityComputeShadowFadeDistance(data.worldPos, zDist);
            ase_lightAtten = UnityMixRealtimeAndBakedShadows(data.atten, bakedAtten, UnityComputeShadowFade(fadeDist));
            #endif
            c.rgb = _ShadowColor.rgb;
            c.a = saturate(((1.0 - ase_lightAtten) * _ShadowStrength));
            return c;
        }
        inline void LightingStandardCustomLighting_GI(inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi)
        {
            s.GIData = data;
        }
        void surf(Input i , inout SurfaceOutputCustomLightingCustom o)
        {
            o.SurfInput = i;
        }
        ENDCG
    }
        Fallback "Diffuse"
            CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18912
-1683.333;13.33333;1630;1365.667;996.8593;638.4348;1;True;False
Node;AmplifyShaderEditor.LightAttenuation;1;-452.5999,-125.8333;Inherit;True;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;16;-218.8593,-126.4348;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-502.6,63.16663;Inherit;False;Property;_ShadowStrength;Shadow Strength;1;0;Create;True;0;0;0;False;0;False;0.5882353;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-42.85925,-127.4348;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;15;105.1407,-127.4348;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;11;42.14075,2.565186;Inherit;False;Property;_ShadowColor;Shadow Color;0;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;302,-264;Float;False;True;-1;2;ASEMaterialInspector;0;0;CustomLighting;ShadowsOnly;False;False;False;False;True;True;True;True;True;True;True;True;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;False;Opaque;;Geometry;All;18;all;True;True;True;False;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;2;5;False;-1;10;False;-1;1;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;2;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;16;0;1;0
WireConnection;18;0;16;0
WireConnection;18;1;5;0
WireConnection;15;0;18;0
WireConnection;0;9;15;0
WireConnection;0;13;11;0
ASEEND*/
//CHKSM=A2737DF874D1AAFE79F9973146D9DCD8D40CC490