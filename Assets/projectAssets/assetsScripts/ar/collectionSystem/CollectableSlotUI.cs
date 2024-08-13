namespace Sugar.CollectionSystem
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    using TMPro;

    public class CollectableSlotUI : MonoBehaviour
    {
        public TextMeshProUGUI itemName;
        public Image collectedIndicator;
        public Button itemInfoButton;

        public CollectableItemData data;

        private void Start()
        {
            itemName.text = data.itemName;
            SetState(false);
        }
        public void SetState(bool stateToSet)
        {
            collectedIndicator.gameObject.SetActive(stateToSet);
            itemInfoButton.interactable = stateToSet;
        }
    }
}
