    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedPanel : PannelBase
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool animateOnDisable = true;
    [SerializeField] private bool animateOnEnable = true;
    [SerializeField] private string showTag = "Show";
    [SerializeField] private string hideTag = "Hide";
    
    
    public override void Enable()
    {
        if (animateOnEnable)
        {
            gameObject.SetActive(true);
            animator.SetTrigger(showTag);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public override void Disable()
    {
        if (animateOnDisable)
        {
            gameObject.SetActive(false);
            animator.SetTrigger(hideTag);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
