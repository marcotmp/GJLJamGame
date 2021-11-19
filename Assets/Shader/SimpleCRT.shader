Shader "Unlit/SimpleCRT"
{
    Properties
    {
        [HideInInspector] _MainTex("Texture", 2D) = "white" {}
        _Value("Value", float) = 0.5
        [Toggle] _Vertical("Vertical", int) = 0
        [Toggle] _RGB("rgb", int) = 0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float _Value;
                int _Vertical;
                int _RGB;

                struct MeshData
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct Interpolators
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                Interpolators vert(MeshData v)
                {
                    Interpolators o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 rgb(uint scan)
                {
                    if (fmod(scan, 3) == 0)
                        return float4(1, _Value, _Value, 1);
                    else if (fmod(scan + 1, 3) == 0)
                        return float4(_Value, 1, _Value, 1);
                    else if (fmod(scan + 2, 3) == 0)
                        return float4(_Value, _Value, 1, 1);
                    else
                        return 1;
                }

                fixed mono(uint scan)
                {
                    if (fmod(scan, 2) == 0)
                        return float4(_Value, _Value, _Value, 1);
                    else
                        return 1;
                }

                fixed4 frag(Interpolators i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv);

                    float scan = 0;
                    if (_Vertical)
                        scan = i.uv.x * _ScreenParams.x;
                    else
                        scan = i.uv.y * _ScreenParams.y;

                    if (_RGB)
                        return col * rgb(scan);
                    else
                        return col * mono(scan);
                }
                ENDCG
            }
        }
}