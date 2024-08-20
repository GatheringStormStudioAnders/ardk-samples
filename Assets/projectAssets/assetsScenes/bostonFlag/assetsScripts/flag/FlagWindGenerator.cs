namespace Sugar.FlagSystem
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    using DG.Tweening;
    public class FlagWindGenerator : MonoBehaviour
    {
        //public Vector2 windForceX;
        //public Vector2 windForceY;
        //public Vector2 windForceZ;

        public Vector3 minWindForce;
        public Vector3 maxWindForce;

        public Vector3 targetWindForce;

        public float windForceDamping;

        public Cloth flagCloth;

        public Vector2 windTimer;
        public float currentTimer;

        private void Start()
        {
            targetWindForce = maxWindForce;
            currentTimer = Random.Range(windTimer.x, windTimer.y);
        }

        private void Update()
        {
            flagCloth.externalAcceleration = Vector3.MoveTowards(flagCloth.externalAcceleration, targetWindForce, windForceDamping * Time.deltaTime);

            if (flagCloth.externalAcceleration == maxWindForce)
            {
                if(currentTimer <= 0)
                {
                    targetWindForce = minWindForce;
                    currentTimer = Random.Range(windTimer.x, windTimer.y);
                }
                else
                {
                    currentTimer -= Time.deltaTime;
                }
            }
            if (flagCloth.externalAcceleration == minWindForce)
            {
                if (currentTimer <= 0)
                {
                    targetWindForce = maxWindForce;
                    currentTimer = Random.Range(windTimer.x, windTimer.y);
                }
                else
                {
                    currentTimer -= Time.deltaTime;
                }
            }
        }
    }
}
