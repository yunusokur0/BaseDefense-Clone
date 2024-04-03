using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void OnChangeAnimationState(AnimEnum animationState)
    {
        animator.SetTrigger(animationState.ToString());
    }

    public void ToggleAttackLayerWeight()
    {
        int attackLayerIndex = animator.GetLayerIndex("Attack");
        float currentWeight = animator.GetLayerWeight(attackLayerIndex);
        float newWeight = (currentWeight == 0f) ? 1f : 0f;
        animator.SetLayerWeight(attackLayerIndex, newWeight);
    }
}
