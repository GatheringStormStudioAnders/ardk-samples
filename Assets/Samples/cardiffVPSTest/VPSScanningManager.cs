using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Niantic.Lightship.AR.LocationAR;
using Niantic.Lightship.AR.PersistentAnchors;

public class VPSScanningManager : MonoBehaviour
{
    public VPSData vpsData;
    public UnityEvent onVPSScanSuccessful;

    public void StartTracking()
    {
        Debug.Log("Tracking");
        vpsData.locationManager.locationTrackingStateChanged += OnLocationTrackingStateChanged;
        vpsData.locationManager.SetARLocations(vpsData.vpsLocations);
        vpsData.locationManager.StartTracking();
    }
    //public IEnumerator InitScanningTrack() //Looking for stability of the tracking.
    //{
    //    while (vpsData.persistentAnchor == null)
    //    {
    //        ARPersistentAnchor searchAnchor = FindObjectOfType<ARPersistentAnchor>();
    //        if (searchAnchor != null)
    //        {
    //            vpsData.persistentAnchor = searchAnchor;
    //        }
    //        yield return new WaitForSeconds(0.5f);
    //    }

    //    while (vpsData.persistentAnchor.trackingState != UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
    //    {
    //        yield return new WaitForSeconds(0.1f);
    //    }

    //    Debug.Log("Successful VPS Scan!");

    //    onVPSScanSuccessful?.Invoke();
    //}

    private void OnLocationTrackingStateChanged(ARLocationTrackedEventArgs args)
    {
        var trackedLocation = args.ARLocation;
        var isTracking = args.Tracking;

        if (isTracking)
        {
            if (!vpsData.isFirstTracking)
            {
                vpsData.isFirstTracking = true;
                onVPSScanSuccessful?.Invoke();
            }
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Debug.Log("PretendSuccess");
    //        onVPSScanSuccessful?.Invoke();
    //    }
    //}
}

[System.Serializable]
public class VPSData
{
    public bool isFirstTracking;
    public ARLocationManager locationManager;
    public ARPersistentAnchor persistentAnchor;
    public ARLocation[] vpsLocations;
}
