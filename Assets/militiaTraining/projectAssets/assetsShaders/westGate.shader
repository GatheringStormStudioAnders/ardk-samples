// Made with Amplify Shader Editor v1.9.3.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "westGate"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Text("Text", 2D) = "white" {}
		_normal("normal", 2D) = "bump" {}
		_occulsion("occulsion", 2D) = "white" {}
		_noiseValue("noiseValue", Float) = 50
		_add("add", Range( 0 , 1)) = 0
		_divide("divide", Range( 0.01 , 3)) = 1
		_heightValues("heightValues", Vector) = (0,0,0,0)
		[HDR]_emission("emission", Color) = (0,0,0,0)
		_offset("offset", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform sampler2D _normal;
		uniform float4 _normal_ST;
		uniform sampler2D _Text;
		uniform float4 _Text_ST;
		uniform float4 _emission;
		uniform float _add;
		uniform float2 _heightValues;
		uniform float _divide;
		uniform float _offset;
		uniform sampler2D _occulsion;
		uniform float4 _occulsion_ST;
		uniform float _noiseValue;
		uniform float _Cutoff = 0.5;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_normal = i.uv_texcoord * _normal_ST.xy + _normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _normal, uv_normal ) );
			float2 uv_Text = i.uv_texcoord * _Text_ST.xy + _Text_ST.zw;
			o.Albedo = tex2D( _Text, uv_Text ).rgb;
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float temp_output_15_0 = saturate( ( ( ase_vertex3Pos.z + (_heightValues.x + (_add - 0.0) * (_heightValues.y - _heightValues.x) / (1.0 - 0.0)) ) / _divide ) );
			float4 lerpResult22 = lerp( _emission , float4( 0,0,0,0 ) , saturate( ( ( 1.0 - temp_output_15_0 ) + _offset ) ));
			o.Emission = lerpResult22.rgb;
			float2 uv_occulsion = i.uv_texcoord * _occulsion_ST.xy + _occulsion_ST.zw;
			o.Occlusion = tex2D( _occulsion, uv_occulsion ).r;
			o.Alpha = 1;
			float simplePerlin2D7 = snoise( i.uv_texcoord*_noiseValue );
			simplePerlin2D7 = simplePerlin2D7*0.5 + 0.5;
			clip( step( temp_output_15_0 , simplePerlin2D7 ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19303
Node;AmplifyShaderEditor.Vector2Node;19;-1072,96;Inherit;False;Property;_heightValues;heightValues;7;0;Create;True;0;0;0;False;0;False;0,0;4,-20;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;14;-1136,0;Inherit;False;Property;_add;add;5;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;17;-864,48;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;4;FLOAT;-5;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;18;-1072,224;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;11;-656,112;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-720,368;Inherit;False;Property;_divide;divide;6;0;Create;True;0;0;0;False;0;False;1;3;0.01;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;12;-400,272;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;15;-176,272;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-208,560;Inherit;False;Property;_offset;offset;9;0;Create;True;0;0;0;False;0;False;0;0.35;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;21;0,176;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-1360,176;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;9;-1376,400;Inherit;False;Property;_noiseValue;noiseValue;4;0;Create;True;0;0;0;False;0;False;50;50;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;208,240;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;7;-1104,384;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;26;336,240;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;20;-224,-192;Inherit;False;Property;_emission;emission;8;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,0;0,2.617681,2.828427,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-1632,-32;Inherit;True;Property;_normal;normal;2;0;Create;True;0;0;0;False;0;False;-1;None;6e5692609ec7a4401be491a51cc6789e;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;1568,-256;Inherit;True;Property;_Text;Text;1;0;Create;True;0;0;0;False;0;False;-1;None;d9319ba77c4534f3cbd41534bd22295c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;16;640,208;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;22;688,80;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;4;1328,288;Inherit;True;Property;_occulsion;occulsion;3;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1904,-16;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;westGate;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;;0;False;;False;0;False;;0;False;;False;0;Custom;0.5;True;True;0;True;Opaque;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;17;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;17;0;14;0
WireConnection;17;3;19;1
WireConnection;17;4;19;2
WireConnection;11;0;18;3
WireConnection;11;1;17;0
WireConnection;12;0;11;0
WireConnection;12;1;13;0
WireConnection;15;0;12;0
WireConnection;21;0;15;0
WireConnection;23;0;21;0
WireConnection;23;1;25;0
WireConnection;7;0;6;0
WireConnection;7;1;9;0
WireConnection;26;0;23;0
WireConnection;16;0;15;0
WireConnection;16;1;7;0
WireConnection;22;0;20;0
WireConnection;22;2;26;0
WireConnection;0;0;2;0
WireConnection;0;1;3;0
WireConnection;0;2;22;0
WireConnection;0;5;4;1
WireConnection;0;10;16;0
ASEEND*/
//CHKSM=4C516EA586A6264D5CB312C37CF51202637C1B6F