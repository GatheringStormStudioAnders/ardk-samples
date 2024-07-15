namespace Sugar.Multiplayer
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.Events;

    using Unity.Netcode;
    using Unity.Netcode.Transports.UTP;

    using Unity.Networking.Transport.Relay;

    using Unity.Services.Core;
    using Unity.Services.Authentication;
    using Unity.Services.Relay;
    using Unity.Services.Relay.Models;

    using TMPro;

    public class RelayManager : MonoBehaviour
    {
        // Start is called before the first frame update

        [Header("Non Dependant UI")]
        public JoinRelayLobbyPanel lobbyPanel;

        [Header("Events")]
        public UnityEvent onLobbyCreated;
        public UnityEvent onLobbyJoined;

        async void Start()
        {
            await UnityServices.InitializeAsync();

            AuthenticationService.Instance.SignedIn += () => { Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId); };
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }


        public async void CreateRelay()
        {
            try
            {
                Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);
                string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

                RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
                NetworkManager.Singleton.StartHost();
                Debug.LogError("joinCode : " + joinCode);
                lobbyPanel.DisplayLobbyCode(joinCode);
                onLobbyCreated?.Invoke();
            }
            catch (RelayServiceException exception)
            {
                Debug.Log(exception);
            }
        }

        public async void JoinRelay(string joinCode)
        {
            try
            {
                Debug.LogError("joining room with code : " + joinCode);
                JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
                RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");

                NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
                NetworkManager.Singleton.StartClient();

                onLobbyJoined?.Invoke();
            }
            catch (RelayServiceException exception)
            {
                Debug.Log(exception);
            }
        }
    }
}