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
        // ����͸�ӹ���״̬
        PXR_Manager.VstDisplayStatusChanged += VstDisplayStatusChanged;

        // ����͸��
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

        //    timeElapsed = 0f;  // ���ü�ʱ��
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
            case PxrVstStatus.Disabled: // �ѹر�
                break;
            case PxrVstStatus.Enabling: // ������
                break;
            case PxrVstStatus.Enabled: // �ѿ���
                break;
            case PxrVstStatus.Disabling: // �ر���
                break;
        }
    }
}
