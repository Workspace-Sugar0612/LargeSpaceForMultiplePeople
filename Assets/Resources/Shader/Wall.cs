using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Material _mat;
    public GameObject _player;

    private void Start()
    {
        _mat = gameObject.GetComponent<MeshRenderer>().materials[0];
        Debug.Log(_player.transform.position);
    }

    private void Update()
    {
        _mat.SetVector("_PlayerPos", _player.transform.position);
    }
}
