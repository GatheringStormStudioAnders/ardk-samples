namespace Sugar.CollectionSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Sugar.UI;
    public class CollectionListBoard : MonoBehaviour
    {
        public CollectionGUI ui;
        public GameObject boardNote;

        public bool isOpen;
        public void TryAccessBoard(Transform target)
        {
            if (!isOpen)
            {
                CollectionListBoard board = target.GetComponent<CollectionListBoard>();
                if (board != null)
                {
                    boardNote.SetActive(false);
                    board.ui.OpenListUI();
                    SetOpenState(true);
                }
            }
        }

        public void SetOpenState(bool state)
        {
            isOpen = state;
        }
    }
}
