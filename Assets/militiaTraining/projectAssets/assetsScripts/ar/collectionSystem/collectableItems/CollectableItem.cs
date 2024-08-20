namespace Sugar.CollectionSystem
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using OutlineSystem;

    public class CollectableItem : MonoBehaviour
    {
        public CollectableItemData data;
        public OutlineObject outline;

        private void Start()
        {
            outline = GetComponentInChildren<OutlineObject>();
        }
    }
}
