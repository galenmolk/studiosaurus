Shader "Custom/BlinkingColor" {
Properties {
    _Color("Color", Color) = (1, 1, 1, 1)
    _BlinkSpeed ("Blink Speed", Range(0, 50.0)) = 1.0
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


            uniform fixed4 _Color;
            uniform fixed _BlinkSpeed;

            struct MeshData {
                float4 vertex : POSITION;
            };

            struct Interpolators {
                float4 vertex : SV_POSITION;
            };
            
            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }
            
            fixed4 frag (Interpolators i) : SV_Target
            {
                fixed blink =  sin(_Time.y * _BlinkSpeed);
                fixed4 color = lerp(_Color, fixed4(0, 0, 0, 1), blink);
                return color;
            }
            ENDCG 
        }
    }
}
}
