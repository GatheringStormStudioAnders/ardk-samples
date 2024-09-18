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
        public GameObject completeGameUI;

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
        public Inspect3DObject inspectSystem;
        public List<CollectableSlotUI> collectableUISlots = new List<CollectableSlotUI>();
        public GameObject closeButton;
        public GameObject noteBackground;

        public int isPickedUp;

        public List<Vector3> listUIPositions = new List<Vector3>();
        public List<Vector3> listUIRotations = new List<Vector3>();
        public List<Transform> listUIParents = new List<Transform>();

        public void UpdateList()
        {
            for (int i = 0; i < collectableUISlots.Count; i++)
            {
                for (int x = 0; x < collectionManager.requiredItems.Count; x++)
                {
                    if (collectionManager.requiredItems[x].data == collectableUISlots[i].data)
                    {
                        collectableUISlots[i].SetState(true);
                        break;
                    }
                }
            }
        }
        public void OpenListUI()
        {
            UpdateList();
            collectionListPanel.transform.localRotation = Quaternion.Euler(Vector3.zero);
            SetListPosition(1);
        }

        public void CloseListUI()
        {
            inspectSystem.enabled = false;
            SetListPosition(0);

        }

        public void SetListPosition(int index)
        {
            collectionListPanel.transform.SetParent(listUIParents[index].transform);
            collectionListPanel.transform.localPosition = listUIPositions[index]; 
            collectionListPanel.transform.localRotation = Quaternion.Euler(listUIRotations[index]);
            if(index == 0)
            {
                closeButton.SetActive(false);
                noteBackground.SetActive(false);
            }
            else 
            {
                closeButton.SetActive(true);
                noteBackground.SetActive(true);
                inspectSystem.enabled = true;
            }
            isPickedUp = index;
        }

        #endregion

        #region Complete Training UI
        [Header("Complete Training UI")]
        public Button closeItemPrompt;
        public void CompleteTraining()
        {
            completeGameUI.SetActive(true);
        }

        #endregion
    }
}
