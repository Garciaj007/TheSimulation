Shader "Custom/WaterShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		[NoScaleOffset] _FlowMap("Flow (RG), A Noise", 2D) = "black" {}
		[NoScaleOffset] _NormalMap("Normal", 2D) = "bump" {}
		_UJump ("U Jump/Phase", Range(-0.25, 0.25)) = 0.25
		_VJump("V Jump/Phase", Range(-0.25, 0.25)) = 0.25
		_Tiling("Tiling", Float) = 1
		_Speed("Speed", Float) = 1
		_FlowStrength("Flow Strength", Float) = 1
		_FlowOffset("Flow Offset", Float) = 1
		_WaterFogColor("Water Fog Color", Color) = (0,0,0,0)
		_WaterFogDensity("Water Fog Density", Range(0, 2)) = 0.1
		_RefractionStrength("Refraction Strength", Range(0, 1)) = 0.25
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 200

		GrabPass{ "_WaterBackground" }

		CGPROGRAM
		#pragma surface surf Standard alpha finalcolor:ResetAlpha
		#pragma target 3.0

		sampler2D _MainTex, _FlowMap, _NormalMap, _CameraDepthTexture, _WaterBackground;
		float4 _CameraDepthTexture_TexelSize;
		float3 _WaterFogColor;
		float _UJump, _VJump, _Tiling, _Speed, _FlowStrength, _FlowOffset, _WaterFogDensity, _RefractionStrength;

		struct Input {
			float2 uv_MainTex;
			float4 screenPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_INSTANCING_BUFFER_END(Props)

		float3 FlowUVW(float2 uv, float2 flowVector, float2 jump, float flowOffset, float tiling, float time, bool flowB) {
			float phaseOffset = flowB ? 0.5 : 0;
			float progress = frac(time + phaseOffset);
			float3 uvw;
			uvw.xy = uv - flowVector * (progress + flowOffset);
			uvw.xy *= tiling;
			uvw.xy += phaseOffset;
			uvw.xy += (time - progress) * jump;
			uvw.z = 1 - abs(1 - 2 * progress);
			return uvw;
		}

		float2 AlignWithGrabTexel(float2 uv) {
			#if UNITY_UV_STARTS_AT_TOP
				if (_CameraDepthTexture_TexelSize.y < 0) {
					uv.y = 1 - uv.y;
				}
			#endif

			return (floor(uv * _CameraDepthTexture_TexelSize.zw) + 0.5) * abs(_CameraDepthTexture_TexelSize.xy);
		}

		float3 ColorBelowWater(float4 screenPos, float3 tangentSpaceNormal) {
			float2 uvOffset = tangentSpaceNormal.xy * _RefractionStrength;
			uvOffset.y *= _CameraDepthTexture_TexelSize.z * abs(_CameraDepthTexture_TexelSize.y);
			float2 uv = AlignWithGrabTexel((screenPos.xy + uvOffset) / screenPos.w);

			#if UNITY_UV_STARTS_AT_TOP
			if (_CameraDepthTexture_TexelSize.y < 0) {
				uv.y = 1 - uv.y;
			}
			#endif

			float backgroundDepth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv));
			float surfaceDepth = UNITY_Z_0_FAR_FROM_CLIPSPACE(screenPos.z);
			float depthDifference = backgroundDepth - surfaceDepth;

			uvOffset *= saturate(depthDifference);
			uv = AlignWithGrabTexel((screenPos.xy + uvOffset) / screenPos.w);
	
			backgroundDepth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv));
			depthDifference = backgroundDepth - surfaceDepth;
			

			float3 backgroundColor = tex2D(_WaterBackground, uv).rgb;
			float fogFactor = exp(-_WaterFogDensity * depthDifference);
			return lerp(_WaterFogColor, backgroundColor, fogFactor);
		}

		void ResetAlpha(Input IN, SurfaceOutputStandard o, inout fixed4 color) {
			color.a = 1;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float2 flowVector = tex2D(_FlowMap, IN.uv_MainTex).rg * 2 - 1;
			flowVector *= _FlowStrength;
			float noise = tex2D(_FlowMap, IN.uv_MainTex).a;
			float time = _Time.y * _Speed + noise;
			float2 jump = float2(_UJump, _VJump);

			float3 uvwA = FlowUVW(IN.uv_MainTex, flowVector, jump, _FlowOffset, _Tiling, time, false);
			float3 uvwB = FlowUVW(IN.uv_MainTex, flowVector, jump, _FlowOffset, _Tiling, time, true);

			float3 normalA = UnpackNormal(tex2D(_NormalMap, uvwA.xy)) * uvwA.z;
			float3 normalB = UnpackNormal(tex2D(_NormalMap, uvwB.xy)) * uvwB.z;

			fixed4 texA = tex2D(_MainTex, uvwA.xy) * uvwA.z;
			fixed4 texB = tex2D (_MainTex, uvwB.xy) * uvwB.z;

			fixed4 c = (texA + texB) * _Color;
			
			o.Albedo = c.rgb;
			o.Normal = normalize(normalA + normalB);
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			o.Emission = ColorBelowWater(IN.screenPos, o.Normal) * (1 - c.a);
		}
		ENDCG
	}
	Fallback Off
}
