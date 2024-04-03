using UnityEngine;


public class BossPhysicsController : MonoBehaviour
{
    [SerializeField] private BossManager bossManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossManager.StartAttack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossManager.StopAllCoroutines();
            bossManager.bossAnimController.OnChangeAnimationState(AnimEnum.Idle);
        }
    }
}
