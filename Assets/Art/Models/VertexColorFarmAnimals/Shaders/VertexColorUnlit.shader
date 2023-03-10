Shader "VertexColorFarmAnimals/VertexColorUnlit" {
Properties {
	_MainTex ("Texture", 2D) = "white" {}
}
 
Category {
	Tags { "Queue"="Geometry" }
	BindChannels {
		Bind "Color", color
		Bind "Vertex", vertex

	}
 
	SubShader {
		Pass {
			
			SetTexture [_MainTex] {
				combine primary 
				}
			
			}
		Pass{
			Tags {"LightMode"="ShadowCaster"}
			}
		}
	}
}