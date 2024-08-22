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

        public List<Vector3> windForces = new List<Vector3>();
        public int currentWindForce;

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

            if (flagCloth.externalAcceleration == targetWindForce)
            {
                if(currentTimer <= 0)
                {
                    List<int> indexesAvailable = new List<int>();

                    for(int x = 0; x < windForces.Count; x++)
                    {
                        indexesAvailable.Add(x);
                    }

                    indexesAvailable.RemoveAt(currentWindForce);
                    currentWindForce = Random.Range(0, indexesAvailable.Count);
                    targetWindForce = windForces[currentWindForce];

                    flagCloth.randomAcceleration = new Vector3(Random.Range(3f, 6f), Random.Range(3f, 6f), 0);

                    if(currentWindForce == 0)
                    {
                        currentTimer = Random.Range(0.5f, 1.5f);
                    }
                    else
                    {
                        currentTimer = Random.Range(windTimer.x, windTimer.y);
                    }
                }
                else
                {
                    currentTimer -= Time.deltaTime;
                }
            }
        }
    }
}
