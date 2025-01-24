using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material skybox1;  // ��һ����պ�

    public float changeInterval = 5f;  // �л����ʱ��

    private float timeElapsed = 0f;

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= changeInterval)
        {
            // �л���պ�
            if (RenderSettings.skybox == skybox1)
            {
                RenderSettings.skybox = null;
            }
            else
            {
                RenderSettings.skybox = skybox1;
            }

            timeElapsed = 0f;  // ���ü�ʱ��
        }
    }
}
