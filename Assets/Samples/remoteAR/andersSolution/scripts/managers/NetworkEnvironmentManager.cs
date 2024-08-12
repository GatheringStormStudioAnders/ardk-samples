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
        public void Start()
        {
            NetworkManager.Singleton.OnServerStarted += SpawnNetworkedObjectsList;
            //NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectServerRpc;
        }

        public void SpawnNetworkedObjectsList()
        {

            for(int i = 0; i < levelPrefabs.Count; i++)
            {
                GameObject networkedOject = Instantiate(levelPrefabs[i], networkedParent);
                networkedOject.GetComponent<NetworkObject>().Spawn(true);
                networkedOject.transform.SetParent(networkedParent);
                networkedOject.transform.localPosition = levelPrefabs[i].transform.position;
                PickUpARObject pickUpARObject = networkedOject.GetComponent<PickUpARObject>();
                if(pickUpARObject != null)
                {
                    pickUpARObject.currentLocalPosition.Value = networkedOject.transform.localPosition;
                }
                else
                {
                    GroundItem groundItem = networkedOject.GetComponent<GroundItem>();
                    if(groundItem != null)
                    {
                        groundItem.currentLocalPosition.Value = networkedOject.transform.localPosition;
                    }
                }
            }
        }
    }
}
