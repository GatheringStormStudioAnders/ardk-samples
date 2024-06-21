namespace Sugar.Collision
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BaseCollision : MonoBehaviour
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
