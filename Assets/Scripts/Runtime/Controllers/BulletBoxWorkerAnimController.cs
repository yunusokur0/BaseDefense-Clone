using UnityEngine;


public class BulletBoxWorkerAnimController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetBoolAnim(AnimEnum animType, bool check)
    {
        animator.SetBool(animType.ToString(), check);
    }
}
