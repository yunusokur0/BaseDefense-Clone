using UnityEngine;

public class BossAnimController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PrepareBomb()
    {
   
    }

    public void TriggerToAttack()
    {
     
    }

    public void OnChangeAnimationState(AnimEnum animationState)
    {
        animator.SetTrigger(animationState.ToString());
    }
}
