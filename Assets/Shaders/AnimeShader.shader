Shader"Custom/AnimeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0, 0.1)) = 0.01
    }
 
    SubShader
    {
        Tags { "RenderType"="Opaque" }
LOD200
 
        Cull
Off
        ZWrite
Off
        ZTest
Always
 
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert noambient
 
sampler2D _MainTex;
fixed4 _OutlineColor;
float _OutlineWidth;
 
struct Input
{
    float2 uv_MainTex;
};
 
void vert(inout appdata_full v, out Input o)
{
    UNITY_INITIALIZE_OUTPUT(Input, o);
    o.uv_MainTex = v.texcoord.xy;
}
 
void surf(Input IN, inout SurfaceOutput o)
{
    fixed4 mainColor = tex2D(_MainTex, IN.uv_MainTex);
 
            // Apply cel shading
    float celShade = dot(o.Normal, float3(0, 0, 1));
    mainColor.rgb = ceil(celShade * 4) / 4;
 
            // Apply outline
    float2 ddx = ddx(IN.uv_MainTex);
    float2 ddy = ddy(IN.uv_MainTex);
    float2 outlineUV = IN.uv_MainTex + _OutlineWidth * normalize(ddx + ddy);
    fixed4 outline = tex2D(_MainTex, outlineUV) * _OutlineColor;
    mainColor.rgb += outline.rgb;
 
    o.Albedo = mainColor.rgb;
    o.Alpha = mainColor.a;
}
        ENDCG
    }
}
