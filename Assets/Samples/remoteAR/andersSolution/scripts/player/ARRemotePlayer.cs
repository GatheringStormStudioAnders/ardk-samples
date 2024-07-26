namespace Sugar.Multiplayer
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using Unity.Netcode;
    using Unity.XR.CoreUtils;

    public class ARRemotePlayer : NetworkBehaviour
    {
        public float speed;

        public NetworkObject networkObject;
        private NetworkEnvironmentManager environmentManager;

        public NetworkVariable<int> materialIndex = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

        public List<Material> userMaterials = new List<Material>();

        public Renderer playerMeshRender;
        public PlayerNetworkManager playerNetworkManager;
        public Transform playerARPosition;
        public Transform XROrigin;

        public bool isEditor;

        public Interaction.PickUpARObject pickUpObject;

        private void Start()
        {
            Init();

            transform.SetParent(environmentManager.networkedParent);

            playerNetworkManager.UpdateNetworkedPlayersServerRpc();

            UpdateMaterial();
        }
        public void Init()
        {
#if UNITY_EDITOR
            isEditor = true;
#endif

#if UNITY_STANDALONE_WIN
            isEditor = true;
#endif
            XROrigin = FindObjectOfType<XROrigin>().transform;
            playerARPosition = Camera.main.transform;

            environmentManager = FindObjectOfType<NetworkEnvironmentManager>();
            playerNetworkManager = FindObjectOfType<PlayerNetworkManager>();
            networkObject = GetComponent<NetworkObject>();
        }
        public void Update()
        {
            if (IsOwner)
            {
                if (isEditor)
                {
                    Vector3 moveDir = Vector3.zero;

                    moveDir.x = Input.GetAxisRaw("Horizontal");
                    moveDir.z = Input.GetAxisRaw("Vertical");

                    XROrigin.position += moveDir * speed * Time.deltaTime;
                }

                if (pickUpObject != null)
                {
                    if(pickUpObject.objectData.isPickedUp.Value == true)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            pickUpObject.DropObjectServerRpc();
                            pickUpObject = null;
                        }
                    }
                }
            }

            transform.position = new Vector3(playerARPosition.position.x, playerARPosition.position.y, playerARPosition.position.z);
        }
        public void UpdateMaterial()
        {
            Debug.LogError("Updating Material on : " + transform.name);
            playerMeshRender.material = Instantiate(userMaterials[materialIndex.Value]);
        }
    }
}
