namespace Sugar.CollectionSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    using Sugar.Collision;

    public class CollectableItemTrigger : BaseCollision
    {
        public CollectableItem collectableItem;

        public void Start()
        {
            collectableItem = GetComponentInParent<CollectableItem>();
        }

        public override void TriggerEntered(Collider col)
        {
            base.TriggerEntered(col);
            collectableItem.outline.ChangeOutlineWidth(collectableItem.outline.outlineLimits.y);
        }

        public override void TriggerExited(Collider col)
        {
            base.TriggerExited(col);
            collectableItem.outline.ChangeOutlineWidth(collectableItem.outline.outlineLimits.x);
        }
    }
}
