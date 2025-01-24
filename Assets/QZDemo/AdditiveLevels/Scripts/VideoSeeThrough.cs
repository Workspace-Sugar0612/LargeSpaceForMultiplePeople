using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using Unity.XR.PXR;
using UnityEngine;

public class VideoSeeThrough : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 监听透视功能状态
        PXR_Manager.VstDisplayStatusChanged += VstDisplayStatusChanged;

        // 开启透视
        PXR_Manager.EnableVideoSeeThrough = true;
        //RenderSettings.skybox = null;
    }

    // Update is called once per frame
    void Update()
    {
        //timeElapsed += Time.deltaTime;

        //if (timeElapsed >= changeInterval)
        //{
        //    if (PXR_Manager.EnableVideoSeeThrough)
        //    {
        //        PXR_Manager.EnableVideoSeeThrough = false;
        //        RenderSettings.skybox = SkyBoxMaterial;
        //        MainCamera.clearFlags = CameraClearFlags.Skybox;
        //    }
        //    else
        //    {
        //        PXR_Manager.EnableVideoSeeThrough = true;
        //        RenderSettings.skybox = null;
        //        MainCamera.clearFlags = CameraClearFlags.SolidColor;
        //    }

        //    timeElapsed = 0f;  // 重置计时器
        //}
    }

    public void EnableThrough()
    {
        PXR_Manager.EnableVideoSeeThrough = true;

        QZPlayerScript playerScript = gameObject.GetComponent<QZPlayerScript>();

        GameObject xrOrigin = playerScript.qzPlayerRig.gameObject;
        Camera mainCamera = xrOrigin.GetComponent<XROrigin>().Camera;
        mainCamera.clearFlags = CameraClearFlags.SolidColor;
    }

    public void DisableThrough()
    {
        PXR_Manager.EnableVideoSeeThrough = false;

        QZPlayerScript playerScript = gameObject.GetComponent<QZPlayerScript>();

        GameObject xrOrigin = playerScript.qzPlayerRig.gameObject;
        Camera mainCamera = xrOrigin.GetComponent<XROrigin>().Camera;
        mainCamera.clearFlags = CameraClearFlags.Skybox;
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
