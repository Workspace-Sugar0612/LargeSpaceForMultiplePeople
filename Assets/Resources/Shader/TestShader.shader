// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/TestShader" {
	Properties{
		_MainTex("Texture", 2D) = "white" { }
		_Interval("Interval",Float)=3	//���ʱ��
		_BeginTime("BeginTime",Float)=0	//��ʼʱ��
		_LoopTime("LoopTime",Float)=0.7	//���ε�ѭ��ʱ��
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

			//���㺯��ûʲô�ر�ģ��ͳ���һ��
			v2f vert(appdata_base v)
			{
				v2f o;
				   o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			//���ģ����㺯�������Ʒ����ʱ����λ��
			float inFlash(float2 uv, float interval, float beginTime, float loopTime)
			{
				//����ֵ
				float brightness = 0.0;

				//��ǰʱ��
				float currentTime = _Time.y;

				//��ȡ���ι��յ���ʼʱ��
				float elapsedTime = fmod(currentTime - beginTime, interval);

				if (elapsedTime >= 0.0 && elapsedTime < loopTime)
				{
					//���㵱ǰλ�õ�����
					float position = elapsedTime / loopTime; //��0��1ѭ��
					float boundary = position;
					if (uv.x > boundary - 0.1 && uv.x < boundary + 0.1) //���ⷶΧ
					{
						brightness = 1.0 - abs(uv.x - boundary) * 10.0; //��������
					}
				}

				brightness = clamp(brightness, 0.0, 1.0);
				return brightness;
			}

			float4 frag(v2f i) : COLOR
			{
				float4 outp;
				//����uvȡ��������ɫ
				float4 texCol = tex2D(_MainTex, i.uv);

				//��������
				float tmpBrightness = inFlash(i.uv, _Interval, _BeginTime, _LoopTime);

				//������ɫ��͸����
				outp = texCol + float4(1.0, 1.0, 1.0, 1.0) * tmpBrightness;
				outp.a = texCol.a * 0.85;

				return outp;
			}
		ENDCG
	}
	}
}
