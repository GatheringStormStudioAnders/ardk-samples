namespace Sugar.CollectionSystem
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    [CreateAssetMenu(menuName ="so/militaTraining/collectableItem")]
    public class CollectableItemData : ScriptableObject
    {
        public string itemName;
        public Sprite itemIcon;
        public string description;
    }
}
