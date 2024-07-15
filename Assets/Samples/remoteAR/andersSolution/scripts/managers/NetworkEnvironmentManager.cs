namespace Sugar.Multiplayer
{
    using System.Collections;
    using System.Collections.Generic;

    using Unity.Netcode;

    using UnityEngine;

    using TMPro;

    using Sugar.Multiplayer.Interaction;
    public class NetworkEnvironmentManager : NetworkBehaviour
    {
        public List<ARRemotePlayer> players = new List<ARRemotePlayer>();

        public List<GameObject> levelPrefabs = new List<GameObject>();

        public TextMeshProUGUI arenvironment;
        public void Start()
        {
            NetworkManager.Singleton.OnServerStarted += SpawnBroom;
            //NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectServerRpc;
            arenvironment = GameObject.FindGameObjectWithTag("environmentTransform").GetComponent<TextMeshProUGUI>();
        }

        public void SpawnBroom()
        {
            GameObject broom = Instantiate(levelPrefabs[0], levelPrefabs[0].transform.position, Quaternion.identity, transform);
            broom.GetComponent<NetworkObject>().Spawn(true);
            broom.transform.SetParent(transform);
            broom.transform.localPosition = levelPrefabs[0].transform.position;
            broom.transform.localRotation = levelPrefabs[0].transform.rotation;
            broom.GetComponent<PickUpARObject>().currentLocalPosition = levelPrefabs[0].transform.position;
        }
        //public void SetPlayerWorldPositioning(ARRemotePlayer player)
        //{
        //    player.transform.SetParent(transform);
        //    if (!players.Contains(player))
        //    {
        //        players.Add(player);
        //    }
        //}

        //[ServerRpc(RequireOwnership = false)]
        //public void OnClientConnectServerRpc(ulong clientID)
        //{
        //    for (int i = 0; i < NetworkManager.Singleton.ConnectedClientsList.Count; i++)
        //    {
        //        if (NetworkManager.Singleton.ConnectedClientsList[i].ClientId == clientID)
        //        {
        //            NetworkManager.Singleton.ConnectedClientsList[i].PlayerObject.TrySetParent(transform);
        //        }
        //    }
        //}

        private void Update()
        {
            arenvironment.text = "arEnvir = " + transform.position.ToString();
        }
    }
}
