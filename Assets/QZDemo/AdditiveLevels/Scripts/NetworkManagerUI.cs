using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror
{
    [RequireComponent(typeof(NetworkManager))]
    public class NetworkManagerUI : MonoBehaviour
    {
        NetworkManager manager;

        private void Awake()
        {
            manager = GetComponent<NetworkManager>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}