using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VPSScanningUI : MonoBehaviour
{
    public VPSScanningManager scanningManager;

    public void Start()
    {
        scanningManager.onVPSScanSuccessful.AddListener(SuccessfulScan);
    }

    public void SuccessfulScan()
    {
        //Can do anything in the UI here.
    }
}
