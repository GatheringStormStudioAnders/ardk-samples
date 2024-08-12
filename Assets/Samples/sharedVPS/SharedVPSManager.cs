namespace Sugar.Multiplayer
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    using Niantic.Lightship.AR.PersistentAnchors;

    using Sugar.UI;

    public class SharedVPSManager : MonoBehaviour
    {
        #region Singleton
        public static SharedVPSManager instance;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion

        public UnityEngine.XR.ARSubsystems.TrackingState vpsTrackingState;
        public ARPersistentAnchor persistentAnchor;

        private void Start()
        {
            StartCoroutine(InitScanningTrack());
        }
        public IEnumerator InitScanningTrack() //Looking for stability of the tracking.
        {
            while (persistentAnchor == null)
            {
                ARPersistentAnchor searchAnchor = FindObjectOfType<ARPersistentAnchor>();
                if (searchAnchor != null)
                {
                    persistentAnchor = searchAnchor;
                }
                yield return new WaitForSeconds(0.5f);
            }

            while (persistentAnchor.trackingState != UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void Update()
        {
            vpsTrackingState = persistentAnchor.trackingState;
        }
    }
}
