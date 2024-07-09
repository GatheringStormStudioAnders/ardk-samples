namespace Sugar.Multiplayer
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    using UnityEngine.Networking;
    using Unity.Netcode;
    public class NetworkUI : MonoBehaviour
    {
        public Button host;
        public Button client;

        private void Awake()
        {
            host.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
            client.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
        }
    }
}
