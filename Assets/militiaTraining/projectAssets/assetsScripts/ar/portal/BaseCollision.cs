namespace Sugar.Collision
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Unity.Netcode;

    public class BaseCollision : NetworkBehaviour
    {
        public void OnTriggerEnter(Collider col)
        {
            TriggerEntered(col);
        }

        public virtual void TriggerEntered(Collider col)
        {

        }

        public void OnTriggerExit(Collider col)
        {
            TriggerExited(col);
        }

        public virtual void TriggerExited(Collider col)
        {

        }

        public void OnTriggerStay(Collider col)
        {
            TriggerStay(col);
        }

        public virtual void TriggerStay(Collider col)
        {

        }
    }
}
