Shader "Custom/ColorShader" {
Properties {
    _MainColor("Main Color", Color) = (1, 1, 1, 1)
    _MainTex("Main Texture", 2D) = "" {}
}

Category {
    Tags { "Queue"="Transparent" "RenderType"="Transparent" }
    Blend SrcAlpha OneMinusSrcAlpha 
    ZWrite Off 

    SubShader {
        Pass {
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            uniform fixed4 _MainColor;
            uniform sampler2D _MainTex;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(sin(_Time.y * 10 * i.uv.x), sin(_Time.y * 15  * -i.uv.x), sin(_Time.y * 20 * i.uv.y), 1);
            }
            ENDCG 
        }
    }
}
}
