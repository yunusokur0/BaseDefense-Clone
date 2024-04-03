using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void OnChangeAnimationState(AnimEnum animationState)
    {
        animator.SetTrigger(animationState.ToString());
    }
}
