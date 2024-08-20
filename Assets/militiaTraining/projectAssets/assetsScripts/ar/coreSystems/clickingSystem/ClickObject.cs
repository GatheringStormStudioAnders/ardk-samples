namespace Sugar.ClickingSystem
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    public class ClickObject : MonoBehaviour
    {
        [Header("Raycast Variables")]
        public LayerMask detectionLayers;
        public float distance;
        private Camera mainCamera;

        [Header("Raycast Callbacks")]
        public UnityEvent<Transform> onRaycastSuccessful;
        public UnityEvent onRaycastFailed;

        private void Start()
        {
            mainCamera = Camera.main;
        }
        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                TriggerRaycast();
            }
#else
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
            {
                TriggerRaycast();
            }
#endif
        }
        public void TriggerRaycast()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, distance, detectionLayers))
            {
                Debug.DrawRay(transform.position, ray.direction * distance, Color.green, 2f);
                Debug.Log("Hit transform : " + hit.transform.name);
                onRaycastSuccessful?.Invoke(hit.transform);
            }
            else
            {
                Debug.DrawRay(transform.position, ray.direction * distance, Color.red, 2f);
                onRaycastFailed?.Invoke();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, distance);
        }
    }
}
