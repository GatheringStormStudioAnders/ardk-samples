namespace Sugar.AR.Portal
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ARPortal : MonoBehaviour
    {
        public bool isInsidePortal;

        public GameObject environmentScene;

        public void OnEnterEnvironment()
        {
            environmentScene.gameObject.SetActive(isInsidePortal);
        }
    }
}
