Shader "CameraEffects"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _LensDirtTex ("Lens Dirt Texture", 2D) = "white" {}
        _GrainIntensity ("Grain Intensity", Range(0,1)) = 0.2
        _ChromaticAberrationIntensity ("Chromatic Aberration Intensity", Range(0,5)) = 1
        _VignetteIntensity ("Vignette Intensity", Range(0,1)) = 0.4
        _DesaturationAmount ("Desaturation Amount", Range(0,1)) = 0.3
        _BloomIntensity ("Bloom Intensity", Range(0,2)) = 0.5
        _LensDirtIntensity ("Lens Dirt Intensity", Range(0,1)) = 0.3

        _BlurSize ("Blur Size", Range(0,10)) = 1.5
        _BarrelDistortion ("Barrel Distortion", Range(-1,1)) = 0.1
        _WobbleIntensity ("Wobble Intensity", Range(0,1)) = 0.2

        _EnableVHS ("Enable VHS", Float) = 1
        _VHSNoiseStrength ("VHS Noise Strength", Range(0,1)) = 0.25
        _ScanlineIntensity ("Scanline Intensity", Range(0,1)) = 0.2
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _LensDirtTex;

            float _GrainIntensity;
            float _ChromaticAberrationIntensity;
            float _VignetteIntensity;
            float _DesaturationAmount;
            float _BloomIntensity;
            float _LensDirtIntensity;

            float _BlurSize;
            float _BarrelDistortion;
            float _WobbleIntensity;

            float _EnableVHS;
            float _VHSNoiseStrength;
            float _ScanlineIntensity;

            // Simple random function based on UV and time
            float rand(float2 co)
            {
                return frac(sin(dot(co.xy ,float2(12.9898,78.233))) * 43758.5453);
            }

            // 9-sample box blur for simple manual DOF approximation
            float3 blur9(sampler2D tex, float2 uv, float2 texelSize, float radius)
            {
                float3 col = float3(0,0,0);
                float count = 0;

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <=1; y++)
                    {
                        float2 offset = float2(x,y) * texelSize * radius;
                        col += tex2D(tex, uv + offset).rgb;
                        count += 1.0;
                    }
                }
                return col / count;
            }

            fixed4 frag(v2f_img i) : SV_Target
            {
                float2 uv = i.uv;

                // Apply rolling shutter wobble (small vertical wave)
                uv.y += sin(uv.x * 50.0 + _Time * 10.0) * 0.001 * _WobbleIntensity;

                // Apply barrel distortion
                float2 centeredUV = uv - 0.5;
                float r2 = dot(centeredUV, centeredUV);
                uv = 0.5 + centeredUV * (1.0 + _BarrelDistortion * r2);

                // Clamp UV after distortion to avoid artifacts
                uv = clamp(uv, 0.0, 1.0);

                // Chromatic aberration offsets (horizontal)
                float2 offsetR = float2(_ChromaticAberrationIntensity * 0.001, 0);
                float2 offsetB = float2(-_ChromaticAberrationIntensity * 0.001, 0);

                float3 colR = tex2D(_MainTex, uv + offsetR).rgb;
                float3 colG = tex2D(_MainTex, uv).rgb;
                float3 colB = tex2D(_MainTex, uv + offsetB).rgb;

                float3 color = float3(colR.r, colG.g, colB.b);

                // Grain effect
                float grain = (rand(uv * _Time.y) - 0.5) * _GrainIntensity;
                color += grain;

                // Vignette
                float dist = distance(uv, float2(0.5, 0.5));
                float vignette = smoothstep(0.8, 0.5, dist);
                color *= lerp(1.0, vignette, _VignetteIntensity);

                // Desaturation
                float gray = dot(color, float3(0.299, 0.587, 0.114));
                color = lerp(color, float3(gray, gray, gray), _DesaturationAmount);

                // Bloom (simple bright boost)
                float brightness = max(max(color.r, color.g), color.b);
                float bloom = saturate((brightness - 0.7) * 5) * _BloomIntensity;
                color += bloom;

                // Lens dirt overlay
                float3 lensDirt = tex2D(_LensDirtTex, uv).rgb * _LensDirtIntensity;
                color += lensDirt;

                // Manual blur for DOF (simple box blur)
                if (_BlurSize > 0)
                {
                    float2 texelSize = float2(1.0 / _ScreenParams.x, 1.0 / _ScreenParams.y);
                    float3 blurred = blur9(_MainTex, uv, texelSize, _BlurSize);
                    // Blend blurred color with original based on blur size (clamped)
                    float blurAmount = saturate(_BlurSize / 10.0);
                    color = lerp(color, blurred, blurAmount);
                }

                // VHS Noise
                if (_EnableVHS > 0.5)
                {
                    float noise = (rand(uv * _Time * 100.0) - 0.5) * _VHSNoiseStrength;
                    color += noise;

                    // Scanlines (horizontal lines)
                    float scanline = sin(uv.y * 800.0) * 0.5 + 0.5;
                    scanline = lerp(1.0, scanline, _ScanlineIntensity);
                    color *= scanline;
                }

                // Clamp final color
                color = saturate(color);

                return fixed4(color, 1.0);
            }
            ENDCG
        }
    }
}