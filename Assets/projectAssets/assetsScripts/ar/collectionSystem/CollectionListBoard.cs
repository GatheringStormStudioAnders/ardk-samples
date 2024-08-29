namespace Sugar.CollectionSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Sugar.UI;
    public class CollectionListBoard : MonoBehaviour
    {
        public CollectionGUI ui;

        public bool isOpen;
        public void TryAccessBoard(Transform target)
        {
            if (!isOpen)
            {
                CollectionListBoard board = target.GetComponent<CollectionListBoard>();
                if (board != null)
                {
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
