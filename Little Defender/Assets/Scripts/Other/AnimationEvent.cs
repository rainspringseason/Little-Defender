using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    public UnityEvent onAnimationStart;
    public UnityEvent onAnimationMiddle;
    public UnityEvent onAnimationEnd;

    public void AnimationStart()
    {
        onAnimationStart?.Invoke();
    }

    public void AnimationMiddle()
    {
        onAnimationMiddle?.Invoke();
    }

    public void AnimationEnd()
    {
        onAnimationEnd?.Invoke();
    }
}
