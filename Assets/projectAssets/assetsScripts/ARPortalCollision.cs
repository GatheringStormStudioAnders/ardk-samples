namespace Sugar.Collision
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    using Sugar.AR.Portal;

    public class ARPortalCollision : BaseCollision
    {
        public ARPortal portal;

        public override void TriggerStay(Collider col)
        {
            portal.isInsidePortal = true;
        }

        public override void TriggerExited(Collider col)
        {
            portal.isInsidePortal = false;
        }
    }
}
