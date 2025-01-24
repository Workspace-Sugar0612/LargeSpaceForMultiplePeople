using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;

public class SeeThroughTest : MonoBehaviour
{
    public GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        PXR_Manager.VstDisplayStatusChanged += VstDisplayStatusChanged;

        // 启动一个定时任务
        //StartCoroutine(StartTimer());

        PXR_Manager.EnableVideoSeeThrough = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 创建一个定时任务，每隔30秒执行一次
    private IEnumerator StartTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            Debug.Log("Timer");

            // 每次执行定时任务时，设置EnableVideoSeeThrough为前一次的相反值
            if(PXR_Manager.EnableVideoSeeThrough)
            {
                PXR_Manager.EnableVideoSeeThrough = false;

                // 设置wall的材质属性
                Color redColor = new Color(1, 1, 0, 1.0f);
                wall.GetComponent<MeshRenderer>().material.color = redColor;

            }
            else
            {
                PXR_Manager.EnableVideoSeeThrough = true;
                // 设置wall的材质属性
                Color redColor = new Color(1, 1, 0, 0.2f);
                wall.GetComponent<MeshRenderer>().material.color = redColor;
            }
        }
    }

    private void VstDisplayStatusChanged(PxrVstStatus status)
    {
        switch (status)
        {
            case PxrVstStatus.Disabled: // 已关闭
                break;
            case PxrVstStatus.Enabling: // 开启中
                break;
            case PxrVstStatus.Enabled: // 已开启
                break;
            case PxrVstStatus.Disabling: // 关闭中
                break;
        }
    }
}
