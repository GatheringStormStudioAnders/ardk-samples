namespace Sugar.AR.Portal
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ARPortal : MonoBehaviour
    {
        public bool isInsidePortal;

        public PortalEnvironment environmentToUse;
        public List<PortalEnvironment> environmentLibrary = new List<PortalEnvironment>();

        public GameObject insidePortalDoorFrame;

        private void Start()
        {
            Application.targetFrameRate = 60;
        }
        public void OnEnterEnvironment()
        {
            environmentToUse.environment.SetActive(isInsidePortal);
            ChangeBacksidePortalDoorFrameLighting();
        }

        public void ChangeBacksidePortalDoorFrameLighting()
        {

            if (isInsidePortal)
            {
                insidePortalDoorFrame.layer = 0;
            }
            else
            {
                insidePortalDoorFrame.layer = 11;
            }
        }

        public void SelectEnvironment(int index)
        {
            Debug.Log("index : " + index);
            for(int i = 0; i < environmentLibrary.Count; i++)
            {
                environmentLibrary[i].parent.SetActive(false);
            }

            environmentLibrary[index].parent.SetActive(true);
            environmentToUse = environmentLibrary[index];
        }
    }

    [System.Serializable]
    public class PortalEnvironment
    {
        public GameObject parent;
        public GameObject environment;
        public GameObject mask;
    }
}
