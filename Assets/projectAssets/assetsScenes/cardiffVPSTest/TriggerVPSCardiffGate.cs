using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

public class TriggerVPSCardiffGate : MonoBehaviour
{
    public float fadeTime;
    public Material westGateMaterial;

    void Start()
    {
        westGateMaterial.SetFloat("_add", 0);
    }

    public void FadeWestGate(float endValue)
    {
        westGateMaterial.DOFloat(endValue, "_add", fadeTime);
    }
}
