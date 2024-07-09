namespace Sugar.Multiplayer
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using Unity.Netcode;

    public class ARRemotePlayer : NetworkBehaviour
    {
        public float speed;

        private NetworkEnvironmentManager environmentManager;

        public Renderer playerMeshRender;

        private void Start()
        {
            environmentManager = FindObjectOfType<NetworkEnvironmentManager>();
            environmentManager.SetPlayerServerRPC(this, OwnerClientId);
        }
        public void Update()
        {
            if (IsOwner)
            {
                Vector3 moveDir = Vector3.zero;

                moveDir.x = Input.GetAxisRaw("Horizontal");
                moveDir.z = Input.GetAxisRaw("Vertical");

                transform.localPosition += moveDir * speed * Time.deltaTime;
            }
            else
            {
                return;
            }
        }

        public void CreateUniqueMaterial(ARRemotePlayer player, Material material)
        {
            player.playerMeshRender.material = material;
        }
    }
}
