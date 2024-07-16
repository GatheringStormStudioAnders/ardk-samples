namespace Sugar.Multiplayer
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    using Unity.Netcode;

    public class PlayerNetworkManager : NetworkBehaviour
    {
        public List<int> uniquePlayerIndex = new List<int>();

        [ServerRpc(RequireOwnership = false)]
        public void UpdateNetworkedPlayersServerRpc()
        {
            for(int i = 0; i < NetworkManager.Singleton.ConnectedClientsList.Count; i++)
            {
                ARRemotePlayer arPlayer = NetworkManager.Singleton.ConnectedClientsList[i].PlayerObject.GetComponent<ARRemotePlayer>();
                arPlayer.materialIndex.Value = uniquePlayerIndex[i];
                arPlayer.UpdateMaterial();
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
