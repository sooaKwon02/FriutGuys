Shader "Unlit/OutlineShader"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1) // �⺻ ����
        _MainTex("Base (RGB)", 2D) = "white" {} // �ؽ�ó
    }
        SubShader
    {
        Tags { "Queue" = "Overlay" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color; // ����

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                // �ؽ�ó ���ø�
                float4 texColor = tex2D(_MainTex, i.uv);
                // ���� ����
                return texColor * _Color;
            }
            ENDCG
        }
    }
}