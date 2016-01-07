Shader "Alpha" {
    Properties {
        _Color ("Main Color", Color ) = (1,1,1,1)
        _MainTex ("Base (RGB) Transparency (A)", 2D) = "white" {}
    
    }
    SubShader {
       
        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            Material {
             
                Diffuse [_Color]
                Ambient (1,1,1,1)
            }
            Lighting on
            SetTexture [_MainTex] {
                combine texture * primary double, texture * primary
            }
        }
        
    }
}