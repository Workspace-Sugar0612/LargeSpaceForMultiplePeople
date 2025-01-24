using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material skybox1;  // 第一个天空盒

    public float changeInterval = 5f;  // 切换间隔时间

    private float timeElapsed = 0f;

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= changeInterval)
        {
            // 切换天空盒
            if (RenderSettings.skybox == skybox1)
            {
                RenderSettings.skybox = null;
            }
            else
            {
                RenderSettings.skybox = skybox1;
            }

            timeElapsed = 0f;  // 重置计时器
        }
    }
}
