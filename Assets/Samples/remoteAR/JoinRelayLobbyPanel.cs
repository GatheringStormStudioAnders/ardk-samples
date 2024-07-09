namespace Sugar.Multiplayer
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    using TMPro;

    public class JoinRelayLobbyPanel : MonoBehaviour
    {
        public RelayManager relayManager;

        public TMP_InputField codeInput;

        public TextMeshProUGUI joinCodeDisplay;


        public void OnFinishedCodeInput()
        {
            relayManager.JoinRelay(codeInput.text);
        }

        public void DisplayLobbyCode(string joinCode)
        {
            joinCodeDisplay.text = "Lobby Code : " + joinCode;
            joinCodeDisplay.transform.parent.gameObject.SetActive(true);
        }
    }
}
