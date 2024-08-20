namespace Sugar.CollectionSystem
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    using Sugar.UI;

    public class CollectionManager : MonoBehaviour
    {
        public List<CollectableItem> requiredItems = new List<CollectableItem>();
        public CollectionGUI ui;
        public CollectionListBoard board;
        public void TryCollectItem(Transform target)
        {
            CollectableItem collectable = target.GetComponent<CollectableItem>();

            if(collectable != null)
            {
                if (!requiredItems.Contains(collectable))
                {
                    requiredItems.Add(collectable);
                    if(requiredItems.Count >= 7) //Hardcoded item count this can be done more dynamically.
                    {
                        ui.completeGameUI.SetActive(true);
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
