namespace Sugar.AR.VFX
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.Events;

    using DG.Tweening;

    public class DissolveInWorld : MonoBehaviour
    {
        public UnityEvent dissolveInComplete;
        public UnityEvent dissolveOutComplete;

        public UnityEvent dissolveGhostComplete;

        public Material depthDissolve;
        public Material ghostDissolve;

        public Sequence currentSequence;

        public float dissolveTime;

        public float dampingSpeed;
        public Transform target;

        private void Start()
        {
            depthDissolve.SetFloat("_FadeShift", 0);
            ghostDissolve.SetFloat("_FadeShift", 0);
        }
        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * dampingSpeed);
        }
        public void DissolveWorld(bool state)
        {
            DestroyCurrentTween();

            Sequence newSequence = DOTween.Sequence();

            if (state)
            {
                newSequence.Insert(0, depthDissolve.DOFloat(1, "_FadeShift", dissolveTime));
                newSequence.InsertCallback(dissolveTime * 0.5f, () => dissolveInComplete?.Invoke());
            }
            else
            {
                newSequence.Insert(0, depthDissolve.DOFloat(0, "_FadeShift", dissolveTime));
                newSequence.InsertCallback(dissolveTime, () => dissolveOutComplete?.Invoke());
            }

            newSequence.Play();
        }

        public void DissolveGhost()
        {
            DestroyCurrentTween();

            Sequence newSequence = DOTween.Sequence();

            newSequence.Insert(0, ghostDissolve.DOFloat(1, "_FadeShift", 2));
            newSequence.InsertCallback(2, () => dissolveGhostComplete?.Invoke());
        }

        public void DestroyCurrentTween()
        {
            if (currentSequence != null)
            {
                currentSequence.Kill();
                currentSequence = null;
            }
        }
    }
}
