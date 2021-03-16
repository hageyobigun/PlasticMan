using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation
{
    private Animator animator;
    private Animator a;


    public PlayerAnimation(Animator _animator)
    {
        this.animator = _animator;
    }

    public void SetAnimation(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

}
