using System.Collections;
using System.Collections.Generic;
using Unity.Loading;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    private BoxCollider boxCollider;
    public GameObject Loading;
    public string SceneName;

    private bool LoadingState = false;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        if(boxCollider != null )
        {
            Debug.Log("Box Collider is attached to the object");
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected");

        LoadingState = true;

        // ����loading����ɫ
        Loading.GetComponent<Renderer>().material.color = Color.red;

        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player detected");
            //other.gameObject.transform.position = new Vector3(0, 0, 0);
        }

        // �ȴ�3�����ת��002-Simple����
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {

        yield return new WaitForSeconds(3);

        if(LoadingState) {
            PXR_Manager.EnableVideoSeeThrough = false;
            SceneManager.LoadScene(SceneName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit detected");

        LoadingState = false;

        // ����loading����ɫ
        Loading.GetComponent<Renderer>().material.color = Color.green;

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player detected");
            //other.gameObject.transform.position = new Vector3(0, 0, 0);
        }
    }
}
