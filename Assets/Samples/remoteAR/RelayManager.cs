namespace Sugar.Multiplayer
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
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

        public TextMeshProUGUI joinCodeDisplay;
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
                joinCodeDisplay.text = "joinCode : " + joinCode;
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
            }
            catch (RelayServiceException exception)
            {
                Debug.Log(exception);
            }
        }
    }

}