Shader "Unlit/PlantDroop"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Droop ("Droop", float) = 0
        _DroopAmount ("Droop Amount", float) = 100
        _DroopColour ("Droop Colour", Color) = (1,0.5,0,1)
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType"="Transparent" }
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            float _Droop;
            float _DroopAmount;
            fixed4 _DroopColour;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //v2f j = i;
                //j.uv.y = j.uv.y - (_MainTex_TexelSize.y * _Droop);
                float offset = ((abs(0.5 - i.uv.x)) * 2) * (_MainTex_TexelSize.y * _Droop * _DroopAmount);
                fixed4 colBase = tex2D(_MainTex, i.uv + fixed2(0, offset));
                fixed4 col = lerp(colBase, _DroopColour, _Droop * 0.75);
                col.a = colBase.a;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
