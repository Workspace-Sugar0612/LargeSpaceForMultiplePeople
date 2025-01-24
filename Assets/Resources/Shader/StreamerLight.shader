// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/StreamerLight" {
	Properties{
		_MainTex("Texture", 2D) = "white" { }
		_Angle("Angle", Float) = 45		//�������б�Ƕ�
		_Width("Width",Float)=0.65		//����Ŀ��
		_Interval("Interval",Float)=3	//���ʱ��
		_BeginTime("BeginTime",Float)=0	//��ʼʱ��
		_OffestX("OffestX",Float)=0.6	//����߽��ƫ����
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

			half _Angle;
			half _Width;
			half _Interval;
			half _BeginTime;
			half _OffestX;
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

			//�������ʹ����� frag����֮ǰ�������޷�ʶ��
			//���ģ����㺯�����Ƕȣ�uv,�����x���ȣ��������ʼʱ�䣬ƫ�ƣ�����ѭ��ʱ��
			 float inFlash(float angle,float2 uv,float xLength,int interval,int beginTime, float offX, float loopTime)
			{
				 //����ֵ
				 float brightness = 0;

				 //��б��
				 float angleInRad = 0.0174444 * angle;

				 //��ǰʱ��
				 float currentTime = _Time.y;

				 //��ȡ���ι��յ���ʼʱ��
				 int currentTimeInt = _Time.y / interval;
				 currentTimeInt *= interval;

				 //��ȡ���ι��յ�����ʱ�� = ��ǰʱ�� - ��ʼʱ��
				 float currentTimePassed = currentTime - currentTimeInt;
				 if (currentTimePassed > beginTime)
				 {
					 //�ײ���߽���ұ߽�
					 float xBottomLeftBound;
					 float xBottomRightBound;

					 //�˵�߽�
					 float xPointLeftBound;
					 float xPointRightBound;

					 float x0 = currentTimePassed - beginTime;
					 x0 /= loopTime;

					 //�����ұ߽�
					 xBottomRightBound = x0;

					 //������߽�
					 xBottomLeftBound = x0 - xLength;

					 //ͶӰ��x�ĳ��� = y/ tan(angle)
					 float xProjL;
					 xProjL = (uv.y) / tan(angleInRad);

					 //�˵����߽� = �ײ���߽� - ͶӰ��x�ĳ���
					 xPointLeftBound = xBottomLeftBound - xProjL;
					 //�˵���ұ߽� = �ײ��ұ߽� - ͶӰ��x�ĳ���
					 xPointRightBound = xBottomRightBound - xProjL;

					 //�߽����һ��ƫ��
					 xPointLeftBound += offX;
					 xPointRightBound += offX;

					 //����õ���������
					 if (uv.x > xPointLeftBound && uv.x < xPointRightBound)
					 {
						 //�õ�������������ĵ�
						 float midness = (xPointLeftBound + xPointRightBound) / 2;

						 //�������ĵ�ĳ̶ȣ�0��ʾλ�ڱ�Ե��1��ʾλ�����ĵ�
						 float rate = (xLength - 2 * abs(uv.x - midness)) / (xLength);
						 brightness = rate;
					 }
				 }
				 brightness = max(brightness,0);
				 brightness = min(brightness, 0.75);

				 //������ɫ = ����ɫ * ����
				 float4 col = float4(1,1,1,1) *brightness;
				 return brightness;
			 }

			 float4 frag(v2f i) : COLOR
			 {
				  float4 outp;

			 //����uvȡ��������ɫ���ͳ���һ��
			float4 texCol = tex2D(_MainTex,i.uv);

			//����i.uv�Ȳ������õ�����ֵ
			float tmpBrightness;
			tmpBrightness = inFlash(_Angle,i.uv,_Width,_Interval,_BeginTime,_OffestX,_LoopTime);

			//ͼ�������ж�����Ϊ ��ɫ��A > 0.5,���Ϊ������ɫ+����ֵ
			if (texCol.w > 0.5)
					outp = texCol + float4(1,1,1,1)*tmpBrightness;
			//�հ������ж�����Ϊ ��ɫ��A <=0.5,����հ�
			else
				outp = float4(0,0,0,0);

			outp.w = 0.85;

			return outp;
			}
		ENDCG
	}
	}
}
