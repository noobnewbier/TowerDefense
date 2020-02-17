//reference : https://halisavakis.com/my-take-on-shaders-dissolve-shader/ Noobnewbier did not write this!
Shader "Noobnewbier/Dissolve" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _DissolveGuide("Dissolve Guide (RGB)", 2D) = "white" {}
        _DissolveAmount("Dissolve Amount", Range(0.0, 1.0)) = 0
 
        _DissolveSize("Dissolve Size", Range(0.0, 1.0)) = 0.15
        _DissolveRamp("Dissolve Ramp (RGB)", 2D) = "white" {}
        _DissolveColor("Dissolve Color", Color) = (1,1,1,1)
 
        _EmissionAmount("Emission amount", float) = 2.0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull Off
        CGPROGRAM
        #pragma surface surf Lambert addshadow
        #pragma target 3.0
 
        fixed4 _Color;
        sampler2D _MainTex;
        sampler2D _DissolveGuide;
        sampler2D _BumpMap;
        sampler2D _DissolveRamp;
        fixed4 _DissolveColor;
        float _DissolveSize;
        float _DissolveAmount;
        float _EmissionAmount;
 
        struct Input {
            float2 uv_MainTex;
        };
 
 
        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            half test = tex2D(_DissolveGuide, IN.uv_MainTex).rgb - _DissolveAmount;
            clip(test);
             
            if (test < _DissolveSize && _DissolveAmount > 0) {
                o.Emission = tex2D(_DissolveRamp, float2(test * (1 / _DissolveSize), 0)) * _DissolveColor * _EmissionAmount;
            }
 
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Standard"
}