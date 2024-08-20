namespace Sugar.Collision
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    using Sugar.AR.Portal;

    public class PortalEnvironmentCollision : BaseCollision
    {
        public ARPortal portal;
        public int myEnvironment;
        public override void TriggerEntered(Collider col)
        {
            portal.OnEnterEnvironment();
        }

        public override void TriggerExited(Collider col)
        {
            portal.isInsidePortal = false;
            portal.environmentLibrary[myEnvironment].environment.SetActive(true);
            portal.ChangeBacksidePortalDoorFrameLighting();
        }
    }
}
