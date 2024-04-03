using UnityEngine;

public class MinerAnimController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void OnChangeAnimationState(AnimEnum animationState)
    {
        animator.SetTrigger(animationState.ToString());
    }

    public void SetBoolAnim(AnimEnum animType, bool check)
    {
        animator.SetBool(animType.ToString(), check);
    }

    public void ToggleCarryLayerWeight()
    {
        int carryLayerIndex = animator.GetLayerIndex("Carry");
        float currentWeight = animator.GetLayerWeight(carryLayerIndex);
        float newWeight = (currentWeight == 0f) ? 1f : 0f;
        animator.SetLayerWeight(carryLayerIndex, newWeight);
    }
}
