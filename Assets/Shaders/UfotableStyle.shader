Shader"Custom/UfotableStyle"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0, 0.1)) = 0.01
        _DetailStrength ("Detail Strength", Range(0, 1)) = 0.5
    }
 
    SubShader
    {
        Tags { "RenderType"="Opaque" }
LOD200
 
        Cull
Back
        ZWrite
On
        ZTest
LEqual
 
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert noambient
 
sampler2D _MainTex;
fixed4 _OutlineColor;
float _OutlineWidth;
float _DetailStrength;
 
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
 
            // Apply detail enhancement
    float2 detailUV = IN.uv_MainTex * 10.0;
    float4 detail = tex2D(_MainTex, detailUV);
    mainColor.rgb += (_DetailStrength * detail.rgb);
 
    o.Albedo = mainColor.rgb;
    o.Alpha = mainColor.a;
}
        ENDCG
    }
}
