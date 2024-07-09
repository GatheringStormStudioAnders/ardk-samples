namespace Sugar.Multiplayer
{
    using System.Collections;
    using System.Collections.Generic;

    using Unity.Netcode;

    using UnityEngine;

    public class NetworkEnvironmentManager : MonoBehaviour
    {
        public Transform playerEnvironmentParent;

        public List<ARRemotePlayer> players = new List<ARRemotePlayer>();
        public List<Material> uniquePlayerMaterials = new List<Material>();

        [ServerRpc]
        public void SetPlayerServerRPC(ARRemotePlayer player, ulong clientID)
        {
            player.transform.SetParent(playerEnvironmentParent);
            player.transform.localPosition += new Vector3(0, 1, 0);

            if (!players.Contains(player))
            {
                players.Add(player);
                player.CreateUniqueMaterial(player, uniquePlayerMaterials[Random.Range(0, uniquePlayerMaterials.Count)]);
            }
        }
    }
}
