using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hiden : MonoBehaviour
{
    public GameObject _Origin;
    public GameObject[] _Objects5;
    public GameObject[] _Objects10;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 遍历计算之间的距离 如果距离大于一定值 则隐藏
        foreach (var obj in _Objects5)
        {
            if (Vector3.Distance(_Origin.transform.position, obj.transform.position) > 5)
            {
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(true);
            }
        }

        foreach (var obj in _Objects10)
        {
            if (Vector3.Distance(_Origin.transform.position, obj.transform.position) > 10)
            {
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(true);
            }
        }
    }
}
