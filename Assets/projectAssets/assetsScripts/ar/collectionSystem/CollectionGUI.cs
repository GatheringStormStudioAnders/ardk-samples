namespace Sugar.UI
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    using Sugar.CollectionSystem;
    using TMPro;
    public class CollectionGUI : MonoBehaviour
    {
        public CollectionManager collectionManager;
        #region Info Panel
        [Header("Info Panel UI")]
        public GameObject itemInfoPanel;
        public TextMeshProUGUI itemName;
        public TextMeshProUGUI itemDescription;
        public Image icon;
        public void OpenItemInfo(CollectableItemData collectableItem)
        {
            itemName.text = collectableItem.itemName;
            itemDescription.text = collectableItem.description;
            icon.sprite = collectableItem.itemIcon;

            itemInfoPanel.SetActive(true);
        }

        public void CloseItemInfo()
        {
            itemInfoPanel.SetActive(false);
        }
        #endregion

        #region Item List UI

        [Header("Collection List UI")]
        public GameObject collectionListPanel;
        public List<CollectableSlotUI> collectableUISlots = new List<CollectableSlotUI>();
        public void OpenListUI()
        {
            collectionListPanel.transform.localRotation = Quaternion.Euler(Vector3.zero);
            for(int i = 0; i < collectableUISlots.Count; i++)
            {
                for(int x = 0; x < collectionManager.requiredItems.Count; x++)
                {
                    if(collectionManager.requiredItems[x].data == collectableUISlots[i].data)
                    {
                        collectableUISlots[i].SetState(true);
                        break;
                    }
                }
            }

            collectionListPanel.SetActive(true);
        }

        public void CloseListUI()
        {
            collectionListPanel.SetActive(false);
            collectionManager.board.boardNote.SetActive(true);
        }

        #endregion
    }
}
