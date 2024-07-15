namespace Sugar.Multiplayer
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    using Unity.Netcode;

    public class PlayerNetworkManager : NetworkBehaviour
    {
        public List<int> uniquePlayerIndex = new List<int>();
        public List<NetworkedPlayer> networkedPlayers = new List<NetworkedPlayer>();

        [ServerRpc(RequireOwnership = false)]
        public void FindPlayerObjectsServerRPC()
        {
            NetworkObject[] networkBehaviours = FindObjectsOfType<NetworkObject>();

            for(int i = 0; i < networkBehaviours.Length; i++)
            {
                if (networkBehaviours[i].IsPlayerObject)
                {
                    AddToNetworkedPlayers(networkBehaviours[i]);
                }
            }

            UpdateNetworkedPlayers();
        }

        //[ServerRpc(RequireOwnership = false)]
        //public NetworkedPlayer FetchNetworkedPlayerServerRpc(ulong userID)
        //{
        //    for(int i = 0; i < networkedPlayers.Count; i++)
        //    {
        //        if(networkedPlayers[i] != null)
        //        {
        //            if (networkedPlayers[i].ownerID == userID)
        //            {
        //                return networkedPlayers[i];
        //            }
        //        }
        //    }

        //    return null;
        //}

        public void AddToNetworkedPlayers(NetworkObject playerObject)
        {
            ARRemotePlayer arRemotePlayer = playerObject.GetComponent<ARRemotePlayer>();
            if (arRemotePlayer)
            {
                bool isRegistered = false;
                for(int i = 0; i < networkedPlayers.Count; i++)
                {
                    if(networkedPlayers[i].ownerID == playerObject.OwnerClientId)
                    {
                        isRegistered = true;
                        break;
                    }
                }
                if (!isRegistered)
                {
                    NetworkedPlayer networkedPlayer = new NetworkedPlayer();
                    networkedPlayer.ownerID = playerObject.OwnerClientId;
                    networkedPlayer.playerReference = arRemotePlayer;
                    networkedPlayers.Add(networkedPlayer);

                    if (IsOwnedByServer)
                    {
                        SetPlayerMaterial(arRemotePlayer);
                    }
                    //SetPlayerMaterial(arRemotePlayer);
                }
            }
        }

        public void SetPlayerMaterial(ARRemotePlayer arRemotePlayer)
        {
            int materialIndexPicked = Random.Range(0, uniquePlayerIndex.Count);
            int materialValue = uniquePlayerIndex[materialIndexPicked];

            uniquePlayerIndex.RemoveAt(materialIndexPicked);
            //Debug.LogError("Server picked : " + materialIndexPicked + " on player " + arRemotePlayer.name);
            arRemotePlayer.materialIndex.Value = materialValue;
        }

        public void UpdateNetworkedPlayers()
        {
            for(int i = 0; i < networkedPlayers.Count; i++)
            {
                networkedPlayers[i].playerReference.UpdateMaterialClientRPC();
            }
        }
    }

    [System.Serializable]
    public class NetworkedPlayer
    {
        public ulong ownerID;
        public ARRemotePlayer playerReference;
    }
}
