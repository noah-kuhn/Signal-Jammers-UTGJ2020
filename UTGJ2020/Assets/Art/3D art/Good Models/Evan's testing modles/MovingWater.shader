Shader "Custom/MovingWater"
{
    Properties
    {
        _MainTex ("Base Tex", 2D) = "white" {}
        _Tex2 ("tex two", 2D) = "white" {}
        _Tex3 ("tex three", 2D) = "white" {}
        _AlphCutoff("Alpha Cutoff", Float) = 0.5
        _FlowAngle("Flow Angle", Float) = 0.0
        _Speed1("Base Speed", Float) = 10.0
        _Speed2("mult 1 Speed", Float) = 1.5
        _Speed3("mult 2 Speed", Float) = 3.0
        _Saturation("color saturation", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Cutout" }
        LOD 200

        CGPROGRAM
      
        #pragma surface surf Standard fullforwardshadows

        //#include "UnityShaderVariables.cginc""
        
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _Tex2;
        sampler2D _Tex3;

        struct Input
        {
            float2 uv_MainTex;
        };


        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        float _AlphCutoff;
        float _FlowAngle;
        float _Speed1;
        float _Speed2;
        float _Speed3;
        float _Saturation;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex+half2(cos(_FlowAngle),sin(_FlowAngle))*_Time[1]*_Speed1);

            fixed4 c2 = tex2D(_Tex2, IN.uv_MainTex + half2(cos(_FlowAngle), sin(_FlowAngle)) * ((_Time[1]) * _Speed1 * _Speed2+ 1.346));

            c = c2 + step(_AlphCutoff, 1-c2.a) * c;

            if (c.a < _AlphCutoff)
                discard;


            float cavg = (c.r + c.g + c.b) / 3.0;

            c = lerp(float4(cavg, cavg, cavg, c.a), c, _Saturation);

            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
