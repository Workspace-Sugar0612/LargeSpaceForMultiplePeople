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

        // ����һ����ʱ����
        //StartCoroutine(StartTimer());

        PXR_Manager.EnableVideoSeeThrough = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ����һ����ʱ����ÿ��30��ִ��һ��
    private IEnumerator StartTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            Debug.Log("Timer");

            // ÿ��ִ�ж�ʱ����ʱ������EnableVideoSeeThroughΪǰһ�ε��෴ֵ
            if(PXR_Manager.EnableVideoSeeThrough)
            {
                PXR_Manager.EnableVideoSeeThrough = false;

                // ����wall�Ĳ�������
                Color redColor = new Color(1, 1, 0, 1.0f);
                wall.GetComponent<MeshRenderer>().material.color = redColor;

            }
            else
            {
                PXR_Manager.EnableVideoSeeThrough = true;
                // ����wall�Ĳ�������
                Color redColor = new Color(1, 1, 0, 0.2f);
                wall.GetComponent<MeshRenderer>().material.color = redColor;
            }
        }
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
