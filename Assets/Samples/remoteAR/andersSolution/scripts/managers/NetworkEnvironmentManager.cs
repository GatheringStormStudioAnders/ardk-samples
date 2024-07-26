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
        public Transform networkedParent;

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
            GameObject broom = Instantiate(levelPrefabs[0], networkedParent);
            broom.GetComponent<NetworkObject>().Spawn(true);
            broom.transform.SetParent(networkedParent);
            broom.transform.localPosition = levelPrefabs[0].transform.position;
            //broom.transform.localPosition = levelPrefabs[0].transform.position;
            //broom.transform.localRotation = levelPrefabs[0].transform.rotation;
            PickUpARObject pickUpARObject = broom.GetComponent<PickUpARObject>();
            pickUpARObject.currentLocalPosition.Value = broom.transform.localPosition;
        }

        private void Update()
        {
            arenvironment.text = "arEnvir = " + transform.position.ToString();
        }
    }
}
