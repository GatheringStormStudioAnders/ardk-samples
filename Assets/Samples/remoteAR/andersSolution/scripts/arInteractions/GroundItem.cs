using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Unity.Netcode;

using Sugar.Collision;
using Sugar.Multiplayer;
using Sugar.UI;

public class GroundItem : BaseCollision
{
    [SerializeField]
    public ARRemotePlayer interactedPlayer;
    [SerializeField]
    private InteractionPrompt interactionPrompt;

    public PlayerInventory playerInventory;


    public NetworkVariable<Vector3> currentLocalPosition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    [Header("Item Data")]
    public string itemName;
    public Sprite icon;

    private void Start()
    {
        Init();

        transform.localPosition = currentLocalPosition.Value;
    }
    public void Init()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        interactionPrompt = FindObjectOfType<InteractionPrompt>();
    }
    public override void TriggerEntered(Collider col)
    {
        //Debug.LogError("Col : " + col.transform.name);
        interactedPlayer = col.transform.parent.GetComponent<ARRemotePlayer>();

        if (interactedPlayer != null)
        {
            if (interactedPlayer.IsLocalPlayer)
            {
                interactionPrompt.promptAction.text = "Pick Up - " + itemName;
                interactionPrompt.promptIcon.sprite = icon;
                interactionPrompt.promptButton.gameObject.SetActive(true);

                interactionPrompt.promptButton.onClick.RemoveAllListeners();
                interactionPrompt.promptButton.onClick.AddListener(() => Interact());
            }
        }
    }

    public override void TriggerExited(Collider col)
    {
        if (interactedPlayer != null)
        {
            if (interactedPlayer.transform == col.transform.parent)
            {
                if (interactedPlayer.pickUpObject == this)
                {
                    interactedPlayer.pickUpObject = null;
                }

                interactionPrompt.promptButton.gameObject.SetActive(false);
                interactedPlayer = null;
            }
        }
    }

    public void Interact()
    {
        playerInventory.AddItem(this);
    }

    [ServerRpc(RequireOwnership = false)]
    public void UnloadObjectServerRpc()
    {
        GetComponent<NetworkObject>().Despawn(true);
        Destroy(gameObject);
    }
}
