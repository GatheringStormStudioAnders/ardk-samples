namespace Sugar.OutlineSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    using DG.Tweening;

    using Sugar.Collision;

    using UnityEngine.UI;

    public class OutlineObject : MonoBehaviour
    {
        public Renderer meshRenderer;

        public Tween currentTween;

        public Vector2 outlineLimits;

        private void Start()
        {
            InstanceOutlineMaterial();
            ChangeOutlineWidth(0);
        }

        public void InstanceOutlineMaterial()
        {
            meshRenderer.material = Instantiate(meshRenderer.material);
        }

        public void ChangeOutlineWidth(float endValue)
        {
            if(currentTween != null)
            {
                currentTween.Kill();
                currentTween = null;
            }
            bool state = true;
            if(endValue == 0)
            {
                state = false;
            }
            else
            {
                meshRenderer.enabled = state;
            }
            currentTween = meshRenderer.material.DOFloat(endValue, "_OutlineWidth", 0.5f).OnComplete(() => SetActiveState(state));
        }

        public void SetActiveState(bool state)
        {
            meshRenderer.enabled = state;
        }
    }
}
