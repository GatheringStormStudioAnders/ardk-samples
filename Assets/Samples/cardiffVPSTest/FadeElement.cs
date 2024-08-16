using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using UnityEngine.UI;
using DG.Tweening;

public class FadeElement : MonoBehaviour
{
    public List<MaskableGraphic> elements = new List<MaskableGraphic>();

    public float delayStart;
    public float delayEnd;

    public float startAnimationTime;
    public float endAnimationTime;

    public Vector2 fadeLimits = new Vector2(0, 1);

    public Sequence currentSequence;

    public UnityEvent onAnimationStartFinished;
    public UnityEvent onAnimationEndFinished;
    public void StartAnimation()
    {
        SetActiveState(true);

        Sequence newSequence = DOTween.Sequence();

        for(int i = 0; i < elements.Count; i++)
        {
            newSequence.Insert(delayStart, elements[i].DOFade(fadeLimits.y, startAnimationTime));
        }
        newSequence.InsertCallback(delayStart + startAnimationTime, OnStartFinished);

        KillCurrentSequence(newSequence);
    }
    public void EndAnimation()
    {
        Sequence newSequence = DOTween.Sequence();

        for (int i = 0; i < elements.Count; i++)
        {
            newSequence.Insert(delayEnd, elements[i].DOFade(fadeLimits.x, endAnimationTime));
        }

        newSequence.InsertCallback(delayEnd + endAnimationTime, OnEndFinished);

        KillCurrentSequence(newSequence);
    }
    public void OnStartFinished()
    {
        onAnimationStartFinished?.Invoke();
    }
    public void OnEndFinished()
    {
        SetActiveState(false);
        onAnimationEndFinished?.Invoke();
    }
    public void SetActiveState(bool state)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            elements[i].gameObject.SetActive(state);
        }
    }
    public void KillCurrentSequence(Sequence newSequence)
    {
        if(currentSequence != null)
        {
            currentSequence.Kill();
        }
        currentSequence = newSequence;
        currentSequence.Play();
    }
}
