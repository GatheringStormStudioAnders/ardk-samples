using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
public class PhoneIndicatorAnimator : MonoBehaviour
{
    [Header("Animation")]
    [Range(0.1f, 10)]
    public float animationTime;
    public bool isGoingLeft = true;
    public Sequence currentSequence;

    [Header("Phone UI")]
    public Vector2 phonePosXLimits = new Vector2(-175f, 175f);
    public RectTransform phoneRect;

    [Header("Left Bar")]
    public RectTransform leftBar;
    public Vector2 leftBarSizeLimits;

    [Header("Right Bar")]
    public RectTransform rightBar;
    public Vector2 rightBarSizeLimits;
    public void Start()
    {
        Sequence newSequence = DOTween.Sequence();

        newSequence.Insert(0, phoneRect.DOAnchorPosX(phonePosXLimits.x, animationTime / 2));

        //do bars here

        newSequence.Insert(0, rightBar.DOSizeDelta(new Vector2(rightBarSizeLimits.y, rightBar.sizeDelta.y), animationTime / 2));
        newSequence.Insert(0, leftBar.DOSizeDelta(new Vector2(leftBarSizeLimits.x, leftBar.sizeDelta.y), animationTime / 2));

        newSequence.InsertCallback(animationTime / 2, TriggerNewAnimation);


        KillSequence(newSequence);
    }
    public void TriggerNewAnimation()
    {
        isGoingLeft = !isGoingLeft;
        SetPhoneAnimation(isGoingLeft);
    }
    public void SetPhoneAnimation(bool isGoingLeft) //-1 <, 1 >
    {
        Sequence newSequence = DOTween.Sequence();

        if (isGoingLeft)
        {
            newSequence.Insert(0, phoneRect.DOAnchorPosX(phonePosXLimits.x, animationTime));

            //do bars here

            newSequence.Insert(0, rightBar.DOSizeDelta(new Vector2(rightBarSizeLimits.y, rightBar.sizeDelta.y), animationTime));
            newSequence.Insert(0, leftBar.DOSizeDelta(new Vector2(leftBarSizeLimits.x, leftBar.sizeDelta.y), animationTime));

            newSequence.InsertCallback(animationTime, TriggerNewAnimation);
        }
        else
        {
            newSequence.Insert(0, phoneRect.DOAnchorPosX(phonePosXLimits.y, animationTime));

            //do bars here

            newSequence.Insert(0, rightBar.DOSizeDelta(new Vector2(rightBarSizeLimits.x, rightBar.sizeDelta.y), animationTime));
            newSequence.Insert(0, leftBar.DOSizeDelta(new Vector2(leftBarSizeLimits.y, leftBar.sizeDelta.y), animationTime));

            newSequence.InsertCallback(animationTime, TriggerNewAnimation);
        }

        KillSequence(newSequence);
    }

    public void KillSequence(Sequence newSequence)
    {
        if(currentSequence != null)
        {
            currentSequence.Kill();
        }

        currentSequence = newSequence.Play();
    }
}
