using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disassembly : MonoBehaviour
{
    public GameObject left;
    public GameObject right;

    bool IsDisassembly = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClicked()
    {
        if(IsDisassembly)
        {
            // left的X坐标变为0
            left.transform.localPosition = new Vector3(0, left.transform.localPosition.y, left.transform.localPosition.z);
            // right的X坐标变为0
            right.transform.localPosition = new Vector3(0, right.transform.localPosition.y, right.transform.localPosition.z);

            IsDisassembly = false;
        }
        else
        {
            // left的X坐标变为0.01
            left.transform.localPosition = new Vector3(-0.02f, left.transform.localPosition.y, left.transform.localPosition.z);
            // right的X坐标变为-0.01
            right.transform.localPosition = new Vector3(0.02f, right.transform.localPosition.y, right.transform.localPosition.z);

            IsDisassembly = true;
        }
    }
}
