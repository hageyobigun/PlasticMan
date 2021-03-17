using UnityEngine;

public class EnemyAnimation
{
    private Animator animator;


    public EnemyAnimation(Animator _animator)
    {
        this.animator = _animator;
    }

    public void SetAnimation(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
}
