namespace Sugar.Multiplayer.Interaction
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    using Unity.Netcode;

    using Sugar.Collision;
    using Sugar.UI;

    using TMPro;

    [RequireComponent(typeof(ClientNetworkTransform))]
    public class PickUpARObject : BaseCollision
    {
        public ARObjectData objectData;

        [SerializeField]
        private InteractionPrompt interactionPrompt;

        public NetworkEnvironmentManager environmentManager;

        [SerializeField]
        public ARRemotePlayer interactedPlayer;

        public TextMeshProUGUI posDisplay;

        public NetworkVariable<Vector3> currentLocalPosition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

        public void Start()
        {
            Init();
        }

        public void Init()
        {
            interactionPrompt = FindObjectOfType<InteractionPrompt>();
            environmentManager = FindObjectOfType<NetworkEnvironmentManager>();
            posDisplay = GameObject.FindGameObjectWithTag("broomTransform").GetComponent<TextMeshProUGUI>();
        }

        public override void TriggerEntered(Collider col)
        {
            if (objectData.isPickedUp.Value)
            {
                return;
            }
            //Debug.LogError("Col : " + col.transform.name);
            interactedPlayer = col.transform.parent.GetComponent<ARRemotePlayer>();

            if (interactedPlayer.IsLocalPlayer)
            {
                interactionPrompt.promptButton.gameObject.SetActive(true);

                Debug.LogError("Broom Interacted with  : " + interactedPlayer.networkObject.OwnerClientId);
                interactionPrompt.promptButton.onClick.RemoveAllListeners();
                interactionPrompt.promptButton.onClick.AddListener(() => InteractServerRpc(interactedPlayer.networkObject.OwnerClientId));
            }
        }

        public override void TriggerExited(Collider col)
        {
            if (objectData.isPickedUp.Value)
            {
                return;
            }

            if (interactedPlayer.transform == col.transform.parent)
            {
                interactionPrompt.promptButton.gameObject.SetActive(false);
                interactedPlayer = null;
            }
        }

        [ServerRpc(RequireOwnership = false)]
        public void InteractServerRpc(ulong clientID)
        {
            Debug.LogError("InteractServerRpc - " + clientID);
            if (objectData.isPickedUp.Value)
            {
                return;
            }

            for (int i = 0; i < NetworkManager.Singleton.ConnectedClientsList.Count; i++)
            {
                if(NetworkManager.Singleton.ConnectedClientsList[i].ClientId == clientID)
                {
                    interactedPlayer = NetworkManager.Singleton.ConnectedClientsList[i].PlayerObject.GetComponent<ARRemotePlayer>();
                }
            }

            interactionPrompt.UpdatePrompt(objectData.action, objectData.objectIcon);

            if (interactedPlayer != null)
            {
                objectData.isPickedUp.Value = true;
                InteractClientRpc(objectData.isPickedUp.Value);
                return;
            }
        }

        [ClientRpc]
        public void InteractClientRpc(bool pickedUp)
        {
            objectData.isPickedUp.Value = pickedUp;
        }

        private void Update()
        {
            MoveObjectServerRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        public void MoveObjectServerRpc()
        {
            if (objectData.isPickedUp.Value == true)
            {
                transform.position = interactedPlayer.transform.position;
                currentLocalPosition.Value = transform.position;
            }
            else
            {
                transform.localPosition = currentLocalPosition.Value;
            }
        }
    }

    [System.Serializable]
    public class ARObjectData
    {
        public NetworkVariable<bool> isPickedUp = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
        public Renderer meshRenderer;

        public string action;
        public Sprite objectIcon;
    }
}
