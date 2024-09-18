namespace Sugar.CollectionSystem
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    using Sugar.UI;
    using Sugar.OutlineSystem;

    public class CollectionManager : MonoBehaviour
    {
        public List<CollectableItem> requiredItems = new List<CollectableItem>();
        public CollectionGUI ui;
        public CollectionListBoard board;

        public OutlineObject[] outlineObjects;

        private void Start()
        {
            outlineObjects = FindObjectsOfType<OutlineObject>();
            SetOutlineStatus(false);
        }

        public void SetOutlineStatus(bool state)
        {
            foreach (OutlineObject outlineObject in outlineObjects)
            {
                if (outlineObject != null)
                {
                    outlineObject.outlinable.enabled = state;
                }
            }
        }
        public void TryCollectItem(Transform target)
        {
            CollectableItem collectable = target.GetComponent<CollectableItem>();

            if(collectable != null)
            {
                if (!requiredItems.Contains(collectable))
                {
                    requiredItems.Add(collectable);
                    ui.UpdateList();
                    if (requiredItems.Count >= 7)
                    {
                        ui.closeItemPrompt.onClick.AddListener(() => ui.CompleteTraining());
                    }
                }
                ui.OpenItemInfo(collectable.data);
            }
            else
            {
                Debug.LogError("target is not collectable item - " + target.name);
            }
        }
    }
}
