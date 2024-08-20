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
        public Transform pagesParent;
        public List<Transform> pages = new List<Transform>();
        public GameObject finishPage;
        public Transform progressPipParent;
        public List<GameObject> pipFills = new List<GameObject>();

        private void Start()
        {
            for(int i = 0; i < pagesParent.childCount; i++)
            {
                pages.Add(pagesParent.GetChild(i));
            }

            for (int i = 0; i < progressPipParent.childCount; i++)
            {
                pipFills.Add(progressPipParent.GetChild(i).GetChild(0).gameObject);
            }

            UpdatePipUI();
        }

        public void ChangePage(int direction)
        {
            currentPage += direction;

            if(currentPage < 0)
            {
                currentPage = pages.Count - 1;
            }

            if(currentPage > pages.Count - 1)
            {
                FinishPage();
                return;
            }

            UpdatePageUI();

            UpdatePipUI();
        }

        public void UpdatePageUI()
        {
            for (int i = 0; i < pages.Count; i++)
            {
                if (i == currentPage)
                {
                    pages[i].gameObject.SetActive(true);
                }
                else
                {
                    pages[i].gameObject.SetActive(false);
                }
            }
        }

        public void UpdatePipUI()
        {
            for(int i = 0; i < pipFills.Count; i++)
            {
                if(i == currentPage)
                {
                    pipFills[i].gameObject.SetActive(true);
                }
                else
                {
                    pipFills[i].gameObject.SetActive(false);
                }
            }
        }

        public void FinishPage()
        {
            pages[pages.Count - 1].gameObject.SetActive(false);
            progressPipParent.parent.gameObject.SetActive(false);
            finishPage.gameObject.SetActive(true);
        }
    }
}
