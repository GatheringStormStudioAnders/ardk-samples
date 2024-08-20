using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sugar.AR.Placement;
public class DefaultPlanePlacementUI : MonoBehaviour
{
    public Button lockAR;

    public PlanePlacementAR planePlacement;

    public void LockAR()
    {
        planePlacement.onObjectPlacement?.Invoke();
        lockAR.gameObject.SetActive(false);
    }
}
