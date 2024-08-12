namespace Sugar.UI
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    using TMPro;

    using Sugar.Multiplayer;
    public class SharedVPSUI : MonoBehaviour
    {
        public SharedVPSManager sharedVPSManager;

        public TextMeshProUGUI trackingStatus;

        public GameObject lobbyUI;

        private void Start()
        {
            sharedVPSManager = SharedVPSManager.instance;
            StartCoroutine(WaitForScan());
        }

        private void Update()
        {
            trackingStatus.text = "VPS Status : " + sharedVPSManager.vpsTrackingState.ToString();          
        }

        public IEnumerator WaitForScan()
        {
            while(sharedVPSManager.vpsTrackingState != UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                yield return new WaitForSeconds(0.5f);
            }

            lobbyUI.SetActive(true);
        }
    }
}
