namespace Sugar.AR.Placement
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.XR.ARFoundation;
    using UnityEngine.XR.ARSubsystems;

    [RequireComponent(typeof(ARRaycastManager))]
    public class PlanePlacementAR : MonoBehaviour
    {
        public PlanePlacementARData planePlacement;

        public UnityEvent onBeginPlacement;
        public UnityEvent onObjectPlacement;
        public UnityEvent onEndPlacement;

        public GameObject placementIndicator;
        public Transform arContent;

        public Vector2 scaleLimits;

        public bool debugEditorMode;

        public bool isLocked;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
#if UNITY_EDITOR
            enabled = false;
#endif
#if UNITY_STANDALONE_WIN
            enabled = false;
#endif
            planePlacement.aRRaycastManager = GetComponent<ARRaycastManager>();
            planePlacement.arCamera = FindObjectOfType<ARCameraManager>();
        }

        private void Update()
        {
            if (!isLocked)
            {
                UpdateScaling();
                UpdatePlacementPose();
                UpdatePlacementIndicatorMarker(planePlacement.arCamera.transform);
            }
        }

        public void UpdatePlacementPose()
        {
            Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            planePlacement.aRRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);

            if (hits.Count > 0)
            {
                Pose validPlacementPose = hits[0].pose;
                Vector3 cameraForward = planePlacement.arCamera.transform.forward;
                Vector3 cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                validPlacementPose.rotation = Quaternion.LookRotation(cameraBearing);
                planePlacement.lastValidPlacementPose = validPlacementPose;

                if (placementIndicator != null)
                {
                    placementIndicator.SetActive(true);
                    placementIndicator.transform.position = validPlacementPose.position;
                    placementIndicator.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);

                    arContent.transform.position = placementIndicator.transform.position;
                    arContent.transform.rotation = (placementIndicator.transform.rotation * Quaternion.Euler(0, 180,0));
                }
            }
            else
            {
                placementIndicator.SetActive(false);
            }

            arContent.gameObject.SetActive(placementIndicator.activeSelf);

            if (placementIndicator.activeSelf)
            {
                onBeginPlacement.Invoke();
            }
            else
            {
                onEndPlacement.Invoke();
            }
        }

        public void UpdatePlacementIndicatorMarker(Transform target)
        {
            Vector3 targetPostition = new Vector3(target.position.x, placementIndicator.transform.position.y, target.position.z);
            placementIndicator.transform.LookAt(targetPostition);
        }

        #region Scaling

        public Touch touch0;
        public Touch touch1;

        public float initDistance;
        public Vector3 initScale;

        public void UpdateScaling()
        {
            if (Input.touchCount == 2)
            {
                touch0 = Input.GetTouch(0);
                touch1 = Input.GetTouch(1);

                if(touch0.phase == TouchPhase.Ended || touch0.phase == TouchPhase.Canceled || touch1.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Canceled)
                {
                    return;
                }

                if(touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
                {
                    initDistance = Vector2.Distance(touch0.position, touch1.position);
                    initScale = arContent.localScale;
                }
                else
                {
                    //no need for extra move logic due to the catch at the start.

                    float currentDistance = Vector2.Distance(touch0.position, touch1.position);

                    if(Mathf.Approximately(currentDistance, 0))
                    {
                        return;
                    }

                    float scaleFactor = currentDistance / initDistance;

                    Vector3 newScale = initScale * scaleFactor;

                    float clampedScale = Mathf.Clamp(newScale.x, scaleLimits.x, scaleLimits.y);

                    arContent.localScale = new Vector3(clampedScale, clampedScale, clampedScale);

                }
            }
        }

#endregion

        public void LockAR()
        {
#if UNITY_EDITOR

            this.enabled = false;
            placementIndicator.SetActive(true);


#endif
            isLocked = true;
            placementIndicator.SetActive(false);
            foreach(ARPlane plane in planePlacement.planeManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }
        }

        public void LockContentToLastPose()
        {
            arContent.position = placementIndicator.transform.position;
        }

        public void UnlockARPlacement()
        {
            isLocked = false;
        }
    }

    [System.Serializable]
    public class PlanePlacementARData
    {
        public ARRaycastManager aRRaycastManager;
        public ARCameraManager arCamera;
        public ARPlaneManager planeManager;
        public Pose lastValidPlacementPose;
        public float indicatorRotationDamping;
    }
}
