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
        public void TryAccessBoard(Transform target)
        {
            CollectionListBoard board = target.GetComponent<CollectionListBoard>();
            if(board != null)
            {
                boardNote.SetActive(false);
                board.ui.OpenListUI();
            }
        }
    }
}
