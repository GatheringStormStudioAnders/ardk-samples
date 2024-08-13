namespace Sugar.UI
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class ProgressiveModal : MonoBehaviour
    {
        public int currentPage;
        public List<Transform> pages = new List<Transform>();
        public Transform progressPipParent;
        public List<GameObject> pipFills = new List<GameObject>();

        public void ChangePage(int direction)
        {
            currentPage += direction;

            if(currentPage <= 0)
            {
                currentPage = pages.Count - 2;
            }
        }

        public void UpdatePipUI()
        {
            for(int i = 0; i < pipFills.Count; i++)
            {

            }
        }
    }
}
