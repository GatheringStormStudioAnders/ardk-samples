namespace Sugar.OutlineSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    using DG.Tweening;

    using Sugar.Collision;

    using UnityEngine.UI;

    using EPOOutline;

    public class OutlineObject : MonoBehaviour
    {
        //public Renderer meshRenderer;

        public Tween currentTween;

        public Vector2 outlineLimits = new Vector2(0, 1);

        public Outlinable outlinable;

        private void Start()
        {
            outlinable = GetComponent<Outlinable>();
            InstanceOutlineMaterial();
            ChangeOutlineWidth(0);
        }

        public void InstanceOutlineMaterial()
        {
            //meshRenderer.material = Instantiate(meshRenderer.material);
        }

        public void ChangeOutlineWidth(float endValue)
        {
            Debug.Log("Changing : " + transform.name + " Outline to : " + endValue);
            if(currentTween != null)
            {
                currentTween.Kill();
                currentTween = null;
            }

            //bool state = true;

            //if(endValue == 0)
            //{
            //    state = false;
            //}
            //else
            //{
            //    //outlinable.enabled = state;
            //}
            Color color = new Color(1, 1, 1, endValue);
            //currentTween = meshRenderer.material.DOFloat(endValue, "_OutlineWidth", 0.5f).OnComplete(() => SetActiveState(state));
            currentTween = DOTweenModuleEPO.DOColor(outlinable.OutlineParameters, color, 0.5f).OnComplete(() => SetActiveState(false));
            currentTween.Play();
        }

        public void SetActiveState(bool state)
        {
            //outlinable.enabled = state;
        }
    }
}
