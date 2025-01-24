// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/TestShader" {
	Properties{
		_MainTex("Texture", 2D) = "white" { }
		_Interval("Interval",Float)=3	//间隔时间
		_BeginTime("BeginTime",Float)=0	//开始时间
		_LoopTime("LoopTime",Float)=0.7	//单次的循环时间
	}
		SubShader
	{
	Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
	Blend SrcAlpha OneMinusSrcAlpha
		AlphaTest Greater 0.1
		pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			half _Interval;
			half _BeginTime;
			half _LoopTime;

			sampler2D _MainTex;
			float4 _MainTex_ST;

			struct v2f {
				float4  pos : SV_POSITION;
				float2  uv : TEXCOORD0;
			};

			//顶点函数没什么特别的，和常规一样
			v2f vert(appdata_base v)
			{
				v2f o;
				   o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			//核心：计算函数，控制发光的时间与位置
			float inFlash(float2 uv, float interval, float beginTime, float loopTime)
			{
				//亮度值
				float brightness = 0.0;

				//当前时间
				float currentTime = _Time.y;

				//获取本次光照的起始时间
				float elapsedTime = fmod(currentTime - beginTime, interval);

				if (elapsedTime >= 0.0 && elapsedTime < loopTime)
				{
					//计算当前位置的亮度
					float position = elapsedTime / loopTime; //从0到1循环
					float boundary = position;
					if (uv.x > boundary - 0.1 && uv.x < boundary + 0.1) //发光范围
					{
						brightness = 1.0 - abs(uv.x - boundary) * 10.0; //中心最亮
					}
				}

				brightness = clamp(brightness, 0.0, 1.0);
				return brightness;
			}

			float4 frag(v2f i) : COLOR
			{
				float4 outp;
				//根据uv取得纹理颜色
				float4 texCol = tex2D(_MainTex, i.uv);

				//计算亮度
				float tmpBrightness = inFlash(i.uv, _Interval, _BeginTime, _LoopTime);

				//设置颜色和透明度
				outp = texCol + float4(1.0, 1.0, 1.0, 1.0) * tmpBrightness;
				outp.a = texCol.a * 0.85;

				return outp;
			}
		ENDCG
	}
	}
}
